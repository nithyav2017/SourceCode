using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class Investigator
{
    [Key]
    [Column("InvestigatorID")]
    public int InvestigatorId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactInformation { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Affiliation { get; set; }

    [InverseProperty("Investigator")]
    public virtual ICollection<TrialInvestigator> TrialInvestigators { get; set; } = new List<TrialInvestigator>();
}
