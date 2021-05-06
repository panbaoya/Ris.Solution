using Ris.Dal;
using Ris.Models.Deptment;
using System.Collections.Generic;

namespace Ris.IBll
{
    public interface IDeptmentBll
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="stutas">状态</param>
        /// <returns></returns>
        List<DeptmentModel> GetDeptments(int? stutas=-1);

        /// <summary>
        /// 新增科室
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddDeptment(DeptmentModel model,out string errorMsg);

        /// <summary>
        /// 修改科室
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMsg"></param>
        bool UpdateDeptment(DeptmentModel model, out string errorMsg);
    }
}