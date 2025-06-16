using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; }
        [ForeignKey("Trials")]
        public int TrialId { get;set; }
        public Trials Trials { get; set; }

        public DateOnly EnrollmentDate     { get; set; }
        public string EligibilityStatus { get; set; }   
        public DateTime WithDrawalDate { get; set; }

        [ForeignKey("WithdrawalReasons")]
        public int WithDrawalReasonId { get; set; }
        public virtual WithdrawalReason WithdrawalReasons { get; set; } // Navigation property

        [ForeignKey("Participants")]
        public int ParticipantId { get; set; }
        public Participants Participants { get; set; }



    }
}
