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
        public Trials TrialId { get;set; }
        public DateOnly EnrollmentDate     { get; set; }
        public string EligibilityStatus { get; set; }   
        public DateTime WithDrawalDate { get; set; }

        [ForeignKey("WithdrawalReason")]
        public int WithDrawalReasonId { get; set; }

        public WithdrawalReason WithdrawalReason { get; set; } // Navigation property

        [ForeignKey("Participants")]
        public int ParticipantId { get; set; }

        public Participants participant { get; set; }



    }
}
