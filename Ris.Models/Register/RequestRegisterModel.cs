using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.Register
{
    public class RequestRegisterModel
    {
        public string Name { get; set; }
        public string ImageNumber { get; set; }
        public string PatientType { get; set; }
        public string VisitType { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> CheckType { get; set; }
    }
}
