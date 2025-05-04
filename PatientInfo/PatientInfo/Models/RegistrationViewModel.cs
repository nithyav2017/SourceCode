using System.ComponentModel.DataAnnotations;

namespace PatientInfo.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]      
        public string PIN { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        // Patient details fields
        public string DemographicInfo { get; set; }
        public string HCPSpecialty { get; set; }
        public string Indication { get; set; }
        public string InsuranceType { get; set; }
        public bool ConsentForEmail { get; set; }
        public bool ConsentForText { get; set; }
    }
}
