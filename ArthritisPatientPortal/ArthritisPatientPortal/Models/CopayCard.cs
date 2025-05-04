using System.ComponentModel.DataAnnotations;

namespace ArthritisPatientPortal.Models
{
    public class CopayCard
    {
        public int id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient patient { get; set; }
       
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string BinNumber { get; set; }
        [Required]
        public string PcnNumber { get; set; }
        [Required]
        public string GroupNumber { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public InsuranceType InsuranceType { get; set; }
    }
}
