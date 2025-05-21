using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

[PrimaryKey("TrialId", "InvestigatorId")]
public partial class TrialInvestigator
{
    [Key]
    [Column("TrialID")]
    public int TrialId { get; set; }

    [Key]
    [Column("InvestigatorID")]
    public int InvestigatorId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Role { get; set; }

    [ForeignKey("InvestigatorId")]
    [InverseProperty("TrialInvestigators")]
    public virtual Investigator Investigator { get; set; } = null!;

    [ForeignKey("TrialId")]
    [InverseProperty("TrialInvestigators")]
    public virtual Trial Trial { get; set; } = null!;
}
