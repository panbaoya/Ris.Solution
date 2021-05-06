using Ris.Dal.Entitys;
using Ris.Models.Enums;
using Ris.Models.TypeConfig;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ris.Dal.EntityService
{
    public class TypeConfigService: SugarRepository<tb_TypeConfig>
    {
        public List<tb_TypeConfig> GetListByTypes(RequestTypeConfigModel type)
        {
            Expression<Func<tb_TypeConfig, bool>> expression = x =>1== 1;
            if (!string.IsNullOrEmpty(type.DataCode))
            {
                expression = expression.And(x=>x.DataCode == type.DataCode);
            }
            if (!string.IsNullOrEmpty(type.DataName))
            {
                expression = expression.And(x => x.DataName == type.DataName);
            }
            if (type.DataType.HasValue)
            {
                expression = expression.And(x => x.DataType == (int)type.DataType.Value);
            }
            if (type.Status.HasValue)
            {
                expression = expression.And(x => x.Status == type.Status);
            }
            if (type.IsParent.HasValue)
            {
                expression = expression.And(x => x.IsParent == type.IsParent);
            }
            return GetList(expression);
        }

        public tb_TypeConfig GetByType(RequestTypeConfigModel type)
        {
            Expression<Func<tb_TypeConfig, bool>> expression = x => 1 == 1;
            if (!string.IsNullOrEmpty(type.DataCode))
            {
                expression = expression.And(x => x.DataCode == type.DataCode);
            }
            if (!string.IsNullOrEmpty(type.DataName))
            {
                expression = expression.And(x => x.DataName == type.DataName);
            }
            if (type.DataType.HasValue)
            {
                expression = expression.And(x => x.DataType == (int)type.DataType.Value);
            }
            if (type.Status.HasValue)
            {
                expression = expression.And(x => x.Status == type.Status);
            }
            if (type.IsParent.HasValue)
            {
                expression = expression.And(x => x.IsParent == type.IsParent);
            }
            return GetSingle(expression);
        }
    }
}