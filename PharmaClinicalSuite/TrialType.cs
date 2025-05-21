using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

[Table("TrialType")]
public partial class TrialType
{
    [Key]
    public int TrialTypeId { get; set; }

    [StringLength(100)]
    public string TypeName { get; set; } = null!;

    [InverseProperty("TrialType")]
    public virtual ICollection<Trial> Trials { get; set; } = new List<Trial>();
}
