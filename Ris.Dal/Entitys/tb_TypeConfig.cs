using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ris.Dal.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_TypeConfig")]
    public partial class tb_TypeConfig
    {
        public tb_TypeConfig()
        {

            this.Status = 1;

        }
        /// <summary>
        /// Desc:自增id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// Desc:数据代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DataCode { get; set; }

        /// <summary>
        /// Desc:数据名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DataName { get; set; }

        /// <summary>
        /// Desc:备注说明
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remarks { get; set; }

        /// <summary>
        /// Desc:各种数据类型:性别,就诊类型等
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int DataType { get; set; }

        /// <summary>
        /// Desc:是否有效,1是
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Status { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? IsParent { get; set; }
        public string HisCode { get; set; }
    }
}
