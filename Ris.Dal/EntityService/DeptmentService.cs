using Ris.Dal.Entitys;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Dal.EntityService
{
    public class DeptmentService: SugarRepository<tb_Deptment>
    {
        /// <summary>
        /// 根据状态获取科室
        /// </summary>
        /// <param name="patient">状态</param>
        /// <returns></returns>
        public List<tb_Deptment> GetList(int? status )
        {
            Expression<Func<tb_Deptment, bool>> expression =x=> 1==1;
            if (status.HasValue && status!=-1)
            {
                expression = expression.And(x => x.Status==status);
            }
            return GetList(expression);
        }

        /// <summary>
        /// 主键是否存在
        /// </summary>
        /// <returns>true存在</returns>
        public bool IsExist(string deptCode)
        {
            var entity = GetById(deptCode);
            return entity is null ? false:true ;
        }
    }
}
