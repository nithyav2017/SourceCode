using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite.Models
{
   
    public class TrialInvestigator
    {
        [ForeignKey("Trials")]
        public int TrialId { get; set; }

        [ForeignKey("Investigator")]
        public int InvestigatorId { get; set; }
        public virtual  Trials Trials { get; set; }        
        public virtual Investigator Investigator { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
