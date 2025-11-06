using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaClinicalSuite.Domain.Models
{
    public class AdverseEvents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdverseEventId { get;set; }
        public DateOnly EventDate { get; set; }
        public string Severity { get; set; }
        public string Description { get; set; }
        public string Outcome { get;set; }

        [ForeignKey("Participants")]
        public int ParticipantId { get; set; }
        public Participants Participants { get; set; }

        [ForeignKey("Trials")]
        public int TrialId { get; set; }
        public Trials Trials { get; set; }
    }
}
