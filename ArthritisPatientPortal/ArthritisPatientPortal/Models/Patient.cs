using System.ComponentModel.DataAnnotations;

namespace ArthritisPatientPortal.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string Pin { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        public string HcpSpecialty { get; set; }
        public string Indication { get; set; }
        [Required]
        public InsuranceType InsuranceType { get; set; }
        public bool EmailConsent { get; set; }
        public bool TextConsent { get; set; }
        public string? CopayCardId { get; set; }

        public bool IsSynced { get; set; }
    }

    public enum InsuranceType
    {
        Private,
        Medicare,
        Medicaid,
        Uninsured,
        Other
    }
}
