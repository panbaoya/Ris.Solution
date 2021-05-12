using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ris.Dal.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_Register")]
    public partial class tb_Register
    {
        public tb_Register()
        {

            this.ApplyDate = DateTime.Now;

        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// Desc:病人ID/患者id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PatientID { get; set; }

        /// <summary>
        /// Desc:患者姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:卡号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CardNo { get; set; }

        /// <summary>
        /// Desc:姓名全拼
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PinYin { get; set; }

        /// <summary>
        /// Desc:姓名拼音首拼
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PinYin1 { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Gender { get; set; }

        /// <summary>
        /// Desc:身份证号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IDCard { get; set; }

        /// <summary>
        /// Desc:家庭地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Address { get; set; }

        /// <summary>
        /// Desc:联系方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Phone { get; set; }

        /// <summary>
        /// Desc:邮编
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PostCode { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:生日
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BirthDay { get; set; }

        /// <summary>
        /// Desc:当前年龄
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CurrentAge { get; set; }

        /// <summary>
        /// Desc:影像号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ImageNumber { get; set; }

        /// <summary>
        /// Desc:病人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PatientType { get; set; }
        /// <summary>
        /// Desc:就诊类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string VisitType { get; set; }

        /// <summary>
        /// Desc:开单医院
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BillHospital { get; set; }

        /// <summary>
        /// Desc:申请科室编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ApplyDept { get; set; }

        /// <summary>
        /// Desc:申请科室名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// Desc:申请医师代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ApplyDocter { get; set; }

        /// <summary>
        /// Desc:申请医师名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ApplyDocterName { get; set; }

        /// <summary>
        /// Desc:检查类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CheckType { get; set; }

        /// <summary>
        /// Desc:病床
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Bed { get; set; }

        /// <summary>
        /// Desc:申请时间
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime? ApplyDate { get; set; }

        /// <summary>
        /// Desc:就诊号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CheckNo { get; set; }

        /// <summary>
        /// Desc:检查科室代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CheckDept { get; set; }

        /// <summary>
        /// Desc:检查科室名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CheckDeptName { get; set; }

        /// <summary>
        /// Desc:病区
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Area { get; set; }

        /// <summary>
        /// Desc:临床症状
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Diagnosis { get; set; }

        /// <summary>
        /// Desc:检查设备
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Equipment { get; set; }

        /// <summary>
        /// Desc:检查部位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Position { get; set; }

        /// <summary>
        /// Desc:检查方法
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Method { get; set; }

        /// <summary>
        /// Desc:临床诊断
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Symptom { get; set; }

        /// <summary>
        /// Desc:医生要求/描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DockerAsk { get; set; }

        /// <summary>
        /// Desc:登记状态:已登记,已扫描等状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? CheckStatus { get; set; }

        /// <summary>
        /// Desc:图像状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ImageStatus { get; set; }

    }
}
