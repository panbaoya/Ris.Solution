using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ris.Dal.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_ApplyProjects")]
    public partial class tb_ApplyProjects
    {
        public tb_ApplyProjects()
        {


        }
        /// <summary>
        /// Desc:申请项目id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        public string RegisterID { get; set; }

        /// <summary>
        /// Desc:项目代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ItemCode { get; set; }

        /// <summary>
        /// Desc:姓名名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ItemName { get; set; }

        /// <summary>
        /// Desc:项目单价
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Desc:申请数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Quantity { get; set; }

        /// <summary>
        /// Desc:检查部位编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CheckPointCode { get; set; }

        /// <summary>
        /// Desc:检查部位名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CheckPointName { get; set; }

    }
}
