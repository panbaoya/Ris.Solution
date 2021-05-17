using Ris.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.TypeConfig
{
    public class TypeConfigModel
    {
        public int ID { get; set; }
        public string DataCode { get; set; }
        public string DataName { get; set; }
        public string Remarks { get; set; }
        public TypeConfigEnum DataType { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusName
        {
            get { return Status == 1 ? "启用" : "禁用"; }
            set { Status = value == "启用" ? 1 : 0; }
        }

        public string HisCode { get; set; }
        public int? IsParent { get; set; }
    }
}
