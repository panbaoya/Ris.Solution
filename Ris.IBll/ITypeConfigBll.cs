using Ris.Dal;
using Ris.Models.Enums;
using Ris.Models.InterFaceModel;
using Ris.Models.TypeConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.IBll
{
    /// <summary>
    /// 类型配置接口
    /// </summary>
    public interface ITypeConfigBll
    {
        /// <summary>
        /// 获取配置信息列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<TypeConfigModel> GetTypeConfigs(RequestTypeConfigModel type);

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        TypeConfigModel GetTypeConfig(RequestTypeConfigModel type);

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateTypeConfig(TypeConfigModel model);

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="configModel"></param>
        /// <returns></returns>
        bool AddTypeConfig(TypeConfigModel configModel);

        /// <summary>
        /// 根据his接口添加
        /// </summary>
        /// <param name="configModel"></param>
        /// <returns></returns>
        Task AddTypeConfigByHisAsync(PatientInfo patient);
        bool DelTypeConfig(int id);
    }
}
