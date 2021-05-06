using Nursing.Tools;
using Ris.Dal.Entitys;
using Ris.Dal.EntityService;
using Ris.IBll;
using Ris.Models.Enums;
using Ris.Models.TypeConfig;
using System.Collections.Generic;

namespace Ris.Bll
{
    public class TypeConfigBll : ITypeConfigBll
    {
        TypeConfigService _typeConfigService = null;
        public TypeConfigBll()
        {
            _typeConfigService = new TypeConfigService();
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TypeConfigModel GetTypeConfig(RequestTypeConfigModel type)
        {
            var entity = _typeConfigService.GetByType(type);
            if (entity == null)
            {
                entity = new tb_TypeConfig();
            }
            return entity.MapTo<TypeConfigModel>();
        }

        /// <summary>
        /// 获取配置信息列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<TypeConfigModel> GetTypeConfigs(RequestTypeConfigModel type)
        {
            var entitys = _typeConfigService.GetListByTypes(type);
            return entitys.MapListTo<tb_TypeConfig, TypeConfigModel>();
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="model"></param>w
        /// <returns></returns>
        public bool UpdateTypeConfig(TypeConfigModel model)
        {
            var after = _typeConfigService.GetModel(x=>x.ID==model.ID);
            if (after != null)
            {
                var entity = model.MapTo(after);
                return _typeConfigService.Update(entity);
            }
            else
            {
                var entity = model.MapTo<tb_TypeConfig>();
                return _typeConfigService.Insert(entity)>0;
            }
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="model"></param>w
        /// <returns></returns>
        public bool AddTypeConfig(TypeConfigModel model)
        {
            var after = _typeConfigService.GetModel(x => x.DataCode == model.DataCode&& x.DataType==(int)model.DataType);
            if (after != null)
            {
                return false;
            }
            else
            {
                var entity = model.MapTo<tb_TypeConfig>();
                return _typeConfigService.Insert(entity) > 0;
            }
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelTypeConfig(int id)
        {
            var entity = _typeConfigService.GetById(id);
            entity.Status = entity.Status == 0 ? 1 : 0;
            return _typeConfigService.Update(entity);
        }
    }
}
