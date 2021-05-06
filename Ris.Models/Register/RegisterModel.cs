using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Models.Register
{
    public class RegisterModel
    {
        public string PatientID { get; set; }
        public string Name { get; set; }
        public string CardNo { get; set; }
        public string PinYin { get; set; }
        public string PinYin1 { get; set; }
        public Nullable<int> Gender { get; set; }
        public string IDCard { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string CurrentAge { get; set; }
        public string BirthDay { get; set; }
        public string Email { get; set; }
        public string ImageNumber { get; set; }
        public string PatientType { get; set; }
        public string BillHospital { get; set; }
        public string ApplyDept { get; set; }
        public string ApplyDeptName { get; set; }
        public string ApplyDocter { get; set; }
        public string ApplyDocterName { get; set; }
        public string CheckType { get; set; }
        public string Bed { get; set; }
        public Nullable<System.DateTime> ApplyDate { get; set; }
        public string CheckNo { get; set; }
        public string CheckDept { get; set; }
        public string CheckDeptName { get; set; }
        public string Area { get; set; }
        public string Diagnosis { get; set; }
        public string Equipment { get; set; }
        public string Position { get; set; }
        public string Method { get; set; }
        public string Symptom { get; set; }
        public string DockerAsk { get; set; }
    }
}
