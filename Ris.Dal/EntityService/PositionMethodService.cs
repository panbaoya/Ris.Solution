using Ris.Dal.Entitys;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ris.Dal.EntityService
{
    public class PositionMethodService: SugarRepository<tb_PositionMethod>
    {
        public List<tb_PositionMethod> GetPositionMethods()
        {
            return GetList();
        }

        public List<tb_PositionMethod> GetPositions()
        {
            Expression<Func<tb_PositionMethod, bool>> exp = x => x.IsPosition == 1 && x.Status == 1;
            return GetList(exp);
        }

        public List<tb_PositionMethod> GetMethods(int? positionID=null)
        {
            Expression<Func<tb_PositionMethod, bool>> exp = x => x.IsPosition == 0 && x.Status==1;
            if (positionID.HasValue)
            {
                exp = exp.And(x => x.ParentID == positionID);
            }
            return GetList(exp);
        }
    }
}
