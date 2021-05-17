using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.PositionMethod
{
    public class PositionMethodModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否位置
        /// </summary>
        public int? IsPosition { get; set; }

        /// <summary>
        /// 方法的父 id
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
