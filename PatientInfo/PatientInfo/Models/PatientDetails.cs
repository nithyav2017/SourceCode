using PatientInfo.Data;

namespace PatientInfo.Models
{
    public class PatientDetails
    {
        public int Id { get; set; }
        public string DemographicInfo { get; set; }
        public string HCPSpecialty { get; set; }
        public string Indication { get; set; }
        public string InsuranceType { get; set; }
        public bool ConsentForEmail { get; set; }
        public bool ConsentForText { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
