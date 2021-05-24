using Models.WorkList;
using Ris.Models.Register;
using System.Collections.Generic;

namespace Ris.IBll
{
    /// <summary>
    /// 登记接口
    /// </summary>
    public interface IRegisterBll
    {
        /// <summary>
        /// 获取注册信息,给worklist
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        List<WorklistItem> GetRegisterToWorkList();

        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="patientModel"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        bool Register(RegisterModel patientModel,out string errorMsg);

        /// <summary>
        /// 获取登记信息
        /// </summary>
        /// <returns></returns>
        List<RegisterModel> GetRegisters(RequestRegisterModel request);

        bool ImageStatus(string imageNumber);
        bool UpdateRegister(RegisterModel updateModel, out string errorMsg);
    }
}
