using Ris.Dal;
using Ris.Models.Enums;
using Ris.Models.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.IBll
{
    /// <summary>
    /// 登记接口
    /// </summary>
    public interface IRegisterBll
    {
        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        List<RegisterModel> Getpatients(RequestRegisterModel patient);

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
    }
}
