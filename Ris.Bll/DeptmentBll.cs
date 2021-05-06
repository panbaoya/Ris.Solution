using Nursing.Tools;
using Ris.Dal.Entitys;
using Ris.Dal.EntityService;
using Ris.IBll;
using Ris.Models.Deptment;
using System.Collections.Generic;

namespace Ris.Bll
{
    public class DeptmentBll : IDeptmentBll
    {
        DeptmentService _deptmentService = null;

        public DeptmentBll()
        {
            _deptmentService = new DeptmentService();
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="stutas">状态1为可用/启用</param>
        /// <returns></returns>
        public List<DeptmentModel> GetDeptments(int? status)
        {
            var entitys= _deptmentService.GetList(status);
            return entitys.MapListTo<tb_Deptment, DeptmentModel>();
        }

        /// <summary>
        /// 新增科室
        /// </summary>
        /// <param name="model">科室模型</param>
        /// <returns></returns>
        public bool AddDeptment(DeptmentModel model,out string errorMsg)
        {
            errorMsg = "成功";
            if (string.IsNullOrEmpty(model.DeptCode))
            {
                errorMsg = "科室代码不可为空.";
                return false;
            }
            if (string.IsNullOrEmpty(model.DeptName))
            {
                errorMsg = "科室名称不可为空.";
                return false;
            }
            var exist=_deptmentService.IsExist(model.DeptCode);
            if (exist)
            {
                errorMsg = "科室代码已存在.";
                return false;
            }
            var entity = model.MapTo<tb_Deptment>();
            return _deptmentService.Insert(entity)>0;
        }

        public bool UpdateDeptment(DeptmentModel model, out string errorMsg)
        {
            errorMsg = "成功";
            if (string.IsNullOrEmpty(model.DeptCode))
            {
                errorMsg = "科室代码不可为空.";
                return false;
            }
            if (string.IsNullOrEmpty(model.DeptName))
            {
                errorMsg = "科室名称不可为空.";
                return false;
            }
            var afterEntity = _deptmentService.GetById(model.DeptCode);
            if (afterEntity!=null)
            {
                var entity = model.MapTo(afterEntity);
                return _deptmentService.Update(entity);
            }
            else
            {
                errorMsg = "要修改的科室代码不存在..";
                return false;
            }
        }
    }
}