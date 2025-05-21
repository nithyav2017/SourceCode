using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Models
{
    public class TrialInvestigator
    {
        [ForeignKey("Trials")]
        public int TrialId { get; set; }
        public virtual  Trials Trials { get; set; }

        [ForeignKey("Investigators")]
        public int InvestigatorId { get; set; }
        public virtual Investigators Investigators { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
