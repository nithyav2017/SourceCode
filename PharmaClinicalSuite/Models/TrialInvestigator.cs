using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Models
{
    public class TrialInvestigator
    {
        [ForeignKey("Trials")]
        public int TrialId { get; set; }
        public Trials Trials { get; set; }

        [ForeignKey("Investigators")]
        public int InvestigatorId { get; set; }
        public Investigators Investigators { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
