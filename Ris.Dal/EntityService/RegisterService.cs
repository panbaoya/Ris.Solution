using Ris.Dal.Entitys;
using Ris.Models.Register;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ris.Dal.EntityService
{
    public class RegisterService : SugarRepository<tb_Register>
    {
        /// <summary>
        /// 获取患者列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<tb_Register> GetList(RequestRegisterModel request)
        {
            Expression<Func<tb_Register, bool>> expression = x => 1==1;
            if (!string.IsNullOrEmpty( request.Name))
            {
                expression = expression.And(x => x.Name == request.Name);
            }
            if (!string.IsNullOrEmpty(request.ImageNumber))
            {
                expression = expression.And(x => x.ImageNumber == request.ImageNumber);
            }
            if (!string.IsNullOrEmpty(request.PatientType) && request.PatientType!="全部")
            {
                expression = expression.And(x => x.PatientType == request.PatientType);
            }
            if (!string.IsNullOrEmpty(request.VisitType))
            {
                expression = expression.And(x => x.VisitType == request.VisitType);
            }
            if (request.CheckType!=null && request.CheckType.Count>0)
            {
                expression = expression.And(x => request.CheckType.Contains(x.CheckType));
            }
            if (request.StarDate.HasValue)
            {
                expression = expression.And(x => request.StarDate<=x.ApplyDate);
            }
            if (request.EndDate.HasValue)
            {
                expression = expression.And(x => request.EndDate>=x.ApplyDate);
            }
            return GetList(expression);
        }

        public int InsertProjects(List<tb_ApplyProjects>  applyProjects)
        {
            return db.Insertable<tb_ApplyProjects>(applyProjects).ExecuteCommand();
        }

        public bool RequestIDIsExist(string requestID)
        {
            Expression<Func<tb_Register, bool>> expression = x => x.RequestID== requestID;
            var entity = GetModel(expression);
            return entity is null ? false : true;
        }
    }
}
