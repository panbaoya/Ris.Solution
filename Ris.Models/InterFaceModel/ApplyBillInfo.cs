using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ris.Models.InterFaceModel
{
    [XmlRoot(IsNullable = false, ElementName = "ApplyBillInfo", Namespace = "")]
    public class ApplyBillInfo
    {
        public List<PatientInfo> PatientInfos { get; set; }
    }
    public class PatientInfo
    {
        public string RegCode { get; set; }
        public string BedNo { get; set; }
        public string DoctorName { get; set; }
        public string PatientType { get; set; }
        public string PatientTypeName { get; set; }
        public string OutPatientNo { get; set; }
        public string WardCode { get; set; }
        public string DateOfBirth { get; set; }
        public string RelationshipType { get; set; }
        public string EncounterType { get; set; }
        public string EncounterTypeName { get; set; }
        public string Age { get; set; }
        public string LocationName { get; set; }
        public string RelationshipAdd { get; set; }
        public string RelationshipTel { get; set; }
        public string MIcardNumber { get; set; }
        public string HomeTel { get; set; }
        public string DoctorCode { get; set; }
        public string MedicalCardNumber { get; set; }
        public string LocationCode { get; set; }
        public string RelationshipName { get; set; }
        public string HomeAddress { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string WorkAddress { get; set; }
        public string WardName { get; set; }
        public string PatientName { get; set; }
        public string WorkTel { get; set; }
        public string Wbbh { get; set; }
        public string InPatientNo { get; set; }
        public string SSNumber { get; set; }
        public string DiagnosisCode { get; set; }
        public string DiagnosisName { get; set; }
        public List<ApplyBill> ApplyBills { get; set; }
    }

    public class ApplyBill
    {
        public string Result { get; set; }
        public string RequestStatus { get; set; }
        public string Memo { get; set; }
        public string MedicalHistory { get; set; }
        public string LocationCode { get; set; }
        public string ReqDoctorName { get; set; }
        public string RequestDate { get; set; }
        public string UrgentStatus { get; set; }
        public string ReqLocationCode { get; set; }
        public string LocationName { get; set; }
        public string RequestId { get; set; }
        public string ReqLocationName { get; set; }
        public string ReqDoctorCode { get; set; }
        public string RequestType { get; set; }
        public string ClinicDiagnosis { get; set; }
        public string AllergyHistory { get; set; }
        public List<ApplyItem> ApplyItems { get; set; }
    }

    public class ApplyItem
    {
        public string UnitPrice { get; set; }
        public string CheckPointName { get; set; }
        public string CheckPointCode { get; set; }
        public string Memo { get; set; }
        public string MethodName { get; set; }
        public string ItemName { get; set; }
        public string MethodCode { get; set; }
        public string Quantity { get; set; }
        public string ItemCode { get; set; }
    }
}
