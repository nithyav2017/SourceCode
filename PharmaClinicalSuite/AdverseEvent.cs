using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class AdverseEvent
{
    [Key]
    [Column("AdverseEventID")]
    public int AdverseEventId { get; set; }

    [Column("ParticipantID")]
    public int ParticipantId { get; set; }

    [Column("TrialID")]
    public int TrialId { get; set; }

    public DateOnly EventDate { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Severity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Outcome { get; set; }

    [ForeignKey("ParticipantId")]
    [InverseProperty("AdverseEvents")]
    public virtual Participant Participant { get; set; } = null!;

    [ForeignKey("TrialId")]
    [InverseProperty("AdverseEvents")]
    public virtual Trial Trial { get; set; } = null!;
}
