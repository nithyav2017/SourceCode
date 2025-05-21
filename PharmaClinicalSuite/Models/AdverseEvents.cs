using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaClinicalSuite.Models
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
        public Participants participant { get; set; }

        [ForeignKey("Trials")]
        public int TrialId { get; set; }
        public Trials Trails { get; set; }
    }
}
