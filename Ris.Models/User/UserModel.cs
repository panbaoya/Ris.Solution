using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.User
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int? Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
