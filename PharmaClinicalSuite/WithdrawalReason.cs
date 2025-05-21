using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class WithdrawalReason
{
    [Key]
    [Column("WithdrawalReasonID")]
    public int WithdrawalReasonId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string ReasonDescription { get; set; } = null!;

    [InverseProperty("WithdrawalReason")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
