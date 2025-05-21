using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class Trial
{
    [Key]
    [Column("TrialID")]
    public int TrialId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string TrialName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Phase { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public int TrialTypeId { get; set; }

    [InverseProperty("Trial")]
    public virtual ICollection<AdverseEvent> AdverseEvents { get; set; } = new List<AdverseEvent>();

    [InverseProperty("Trial")]
    public virtual ICollection<CaseReportform> CaseReportforms { get; set; } = new List<CaseReportform>();

    [InverseProperty("Trial")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [InverseProperty("Trial")]
    public virtual ICollection<TrialInvestigator> TrialInvestigators { get; set; } = new List<TrialInvestigator>();

    [ForeignKey("TrialTypeId")]
    [InverseProperty("Trials")]
    public virtual TrialType TrialType { get; set; } = null!;

    [ForeignKey("TrialId")]
    [InverseProperty("Trials")]
    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
}
