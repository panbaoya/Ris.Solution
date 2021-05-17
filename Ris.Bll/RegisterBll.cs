using Models.WorkList;
using Nursing.Tools;
using Ris.Dal.Entitys;
using Ris.Dal.EntityService;
using Ris.IBll;
using Ris.Models.InterFaceModel;
using Ris.Models.Register;
using Ris.Tools;
using Ris.Tools.Nlog;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ris.Bll
{
    public class CurrentUserInfo
    {
        public static string UserName = "admin";
    }
    public class RegisterBll : CurrentUserInfo, IRegisterBll
    {
        RegisterService _registerService = null;
        TypeConfigService _typeConfigService = null;

        public RegisterBll()
        {
            _registerService = new RegisterService();
            _typeConfigService = new TypeConfigService();
        }

        /// <summary>
        /// 获取患者基本信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public List<WorklistItem> GetRegisterToWorkList()
        {
            var models = _registerService.GetListBySql<WorklistItem>("SELECT PatientID,Name AS Surname,CardNo AS AccessionNumber,CASE Gender WHEN 'M' THEN '男' WHEN 'F' THEN '女' END AS Sex,Equipment Modality,ApplyDate ExamDateAndTime,BillHospital AS HospitalName," +
                "CASE BirthDay WHEN '' THEN NULL WHEN NULL THEN NULL ELSE BirthDay END AS DateOfBirth, ApplyDate AS ExamDateAndTime " +
                "FROM dbo.tb_Register");
            return models;
        }

        /// <summary>
        /// 获取患者登记列表
        /// </summary>
        /// <returns></returns>
        public List<RegisterModel> GetRegisters(RequestRegisterModel request)
        {
            var models = _registerService.GetList(request).MapListTo<tb_Register, RegisterModel>();
            models.ForEach(x =>
            {
                x.IDCard = AesUnit.AESDecrypt(x.IDCard, AppConfSetting.AesKey);
            });
            return models;
        }

        public bool ImageStatus(string imageNumber)
        {
            var entity=_registerService.GetModel(x => x.ImageNumber == imageNumber);
            if (entity!=null)
            {
                entity.ImageStatus = "图像已到达";
                return _registerService.Update(entity);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增登记
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public bool Register(RegisterModel registerModel, out string errorMsg)
        {
            if (InputVerification(registerModel, out errorMsg))
            {
                registerModel.PatientID = Guid.NewGuid().ToString("N");
                registerModel.IDCard = AesUnit.AESEncrypt(registerModel.IDCard, AppConfSetting.AesKey);//AppConfSetting.AesKey
                registerModel.ImageStatus = "图像未到达";
                var registerEntity = registerModel.MapTo<RegisterModel, tb_Register>();
                var projectEntitys = registerModel.Projects.MapListTo<ApplyItem, tb_ApplyProjects>();
                projectEntitys.ForEach(x =>
                {
                    x.RegisterID = registerEntity.PatientID;
                });
                bool IsSuccess = false;
                try
                {
                    _registerService.db.Ado.BeginTran();
                    int num=_registerService.Insert(registerEntity);
                    int num2=_registerService.InsertProjects(projectEntitys);
                    _registerService.db.Ado.CommitTran();
                    IsSuccess = true;
                }
                catch (Exception ex)
                {
                    _registerService.db.Ado.RollbackTran();
                    throw ex;
                }
                errorMsg = IsSuccess ? "添加成功." : "添加失败.";
                NLogger.LogInfo(IsSuccess ? $"登记成功,影像号:{registerModel.ImageNumber},操作员{UserName}" : $"登记失败,影像号:{registerModel.ImageNumber},操作员{UserName}", UserName);
                return IsSuccess;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 输入验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        bool InputVerification(RegisterModel model, out string errorMsg)
        {
            errorMsg = "成功";
            if (string.IsNullOrEmpty(model.Diagnosis))
            {
                errorMsg = "诊断不可为空.";
                return false;
            }
            Regex re = new Regex(@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?");//实例化一个Regex对象
            if (!string.IsNullOrEmpty(model.Email) && !re.IsMatch(model.Email))
            {
                errorMsg = "邮箱格式不正确.";
                return false;
            }
            return true;
        }
    }
}
