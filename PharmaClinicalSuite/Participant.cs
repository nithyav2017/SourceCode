using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class Participant
{
    [Key]
    [Column("ParticipantID")]
    public int ParticipantId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactInformation { get; set; }

    [Column(TypeName = "text")]
    public string? MedicalHistory { get; set; }

    [Column(TypeName = "text")]
    public string? Allergies { get; set; }

    [Column("BMI", TypeName = "decimal(5, 2)")]
    public decimal? Bmi { get; set; }

    public string? GuardianInfo { get; set; }

    [InverseProperty("Participant")]
    public virtual ICollection<AdverseEvent> AdverseEvents { get; set; } = new List<AdverseEvent>();

    [InverseProperty("Participant")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [InverseProperty("Participant")]
    public virtual ICollection<ParticipantFormEntry> ParticipantFormEntries { get; set; } = new List<ParticipantFormEntry>();
}
