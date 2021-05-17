using Ris.Models.PositionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.IBll
{
    public interface IPositionMethodBll
    {
        /// <summary>
        /// 获取所有部位/方法
        /// </summary>
        /// <returns></returns>
        List<PositionMethodModel> GetPositionMethods();

        /// <summary>
        /// 获取所有有效部位
        /// </summary>
        /// <returns></returns>
        List<PositionMethodModel> GetPositions();

        /// <summary>
        /// 获取所有有效方法
        /// </summary>
        /// <returns></returns>
        List<PositionMethodModel> GetMethods(int? positionID = null);

        /// <summary>
        /// 新增部位方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddPositionMethod(PositionMethodModel model);
        Task AddPositionMethodByHisAsync(List<PositionMethodModel> model);

        /// <summary>
        /// 修改部位方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdatePositionMethod(PositionMethodModel model);
    }
}
