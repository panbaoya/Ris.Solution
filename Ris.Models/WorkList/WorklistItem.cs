// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Models.WorkList
{

    /// <summary>
    /// This class contains the most important values that are transmitted per worklist
    /// </summary>
    public class WorklistItem
    {
        /// <summary>
        /// 登记号/卡号
        /// </summary>
        public string AccessionNumber { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientID { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string Forename { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string DateOfBirth { get; set; }
        /// <summary>
        /// 转诊医师
        /// </summary>
        public string ReferringPhysician { get; set; }
        /// <summary>
        /// 执行医师
        /// </summary>
        public string PerformingPhysician { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public string Modality { get; set; }
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime ExamDateAndTime { get; set; }
        /// <summary>
        /// 检查诊间
        /// </summary>
        public string ExamRoom { get; set; }
        /// <summary>
        /// 检查描述
        /// </summary>
        public string ExamDescription { get; set; }
        /// <summary>
        /// 报告UID
        /// </summary>
        public string StudyUID { get; set; }
        /// <summary>
        /// 手续ID
        /// </summary>
        public string ProcedureID { get; set; }
        /// <summary>
        /// 手续步骤ID
        /// </summary>
        public string ProcedureStepID { get; set; }
        /// <summary>
        /// 医院名
        /// </summary>
        public string HospitalName { get; set; }
        /// <summary>
        /// 预设的AETtile
        /// </summary>
        public string ScheduledAET { get; set; }
    }
}