using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaClinicalSuite.Domain.Models
{
    public class Investigator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvestigatorId { get; set; }
        public string FirstName      { get; set; }=string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string ContactInformation { get;set; }=string.Empty;
        public string Affiliation { get;set; }=string.Empty;

        public virtual ICollection<TrialInvestigator> TrialsInvestigators { get; set; }

        
    }
}
