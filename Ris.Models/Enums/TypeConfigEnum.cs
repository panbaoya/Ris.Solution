using System.ComponentModel;

namespace Ris.Models.Enums
{
    public enum TypeConfigEnum
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All =10000,
        /// <summary>
        /// 性别
        /// </summary>
        [Description("性别")]
        Gender =10001,
        /// <summary>
        /// 患者类型
        /// </summary>
        [Description("患者类型")]
        PatientType =10002,
        /// <summary>
        /// 医院名称
        /// </summary>
        [Description("医院名称")]
        HospitalInfo = 10003,
        /// <summary>
        /// 检查类型
        /// </summary>
        [Description("检查类型")]
        CheckType = 10004,
        /// <summary>
        /// 检查设备
        /// </summary>
        [Description("检查设备")]
        CheckEqu = 10005,
        /// <summary>
        /// 检查设备
        /// </summary>
        [Description("加密向量")]
        AesIV = 10006,
        /// <summary>
        /// 就诊类型
        /// </summary>
        [Description("就诊类型")]
        VisitType = 10007,
    }
}
