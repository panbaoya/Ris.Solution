using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.Deptment
{
    public class DeptmentModel
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int Status { get; set; } = 1;
        public string StatusName
        {
            get { return Status == 1 ? "启用" : "禁用"; }
            set { Status = value == "启用" ? 1 : 0; }
        }
        public string HisDeptCode { get; set; }
    }
}
