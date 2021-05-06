using Ris.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.TypeConfig
{
    public class RequestTypeConfigModel
    {
        public string DataCode { get; set; }
        public string DataName { get; set; }
        public string Remarks { get; set; }
        public TypeConfigEnum? DataType { get; set; }
        public Nullable<int> Status { get; set; }
        public int? IsParent { get; set; }
    }
}
