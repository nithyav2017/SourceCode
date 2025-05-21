using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class Enrollment
{
    [Key]
    [Column("EnrollmentID")]
    public int EnrollmentId { get; set; }

    [Column("ParticipantID")]
    public int ParticipantId { get; set; }

    [Column("TrialID")]
    public int TrialId { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string EligibilityStatus { get; set; } = null!;

    public DateOnly? WithdrawalDate { get; set; }

    [Column("WithdrawalReasonID")]
    public int? WithdrawalReasonId { get; set; }

    [ForeignKey("ParticipantId")]
    [InverseProperty("Enrollments")]
    public virtual Participant Participant { get; set; } = null!;

    [ForeignKey("TrialId")]
    [InverseProperty("Enrollments")]
    public virtual Trial Trial { get; set; } = null!;

    [ForeignKey("WithdrawalReasonId")]
    [InverseProperty("Enrollments")]
    public virtual WithdrawalReason? WithdrawalReason { get; set; }
}
