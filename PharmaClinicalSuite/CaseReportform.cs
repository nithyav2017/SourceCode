using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

[Table("CaseReportform")]
public partial class CaseReportform
{
    [Key]
    [Column("FormID")]
    public int FormId { get; set; }

    [Column("TrialID")]
    public int TrialId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string FormName { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [InverseProperty("Form")]
    public virtual ICollection<DataCollectionField> DataCollectionFields { get; set; } = new List<DataCollectionField>();

    [InverseProperty("Form")]
    public virtual ICollection<ParticipantFormEntry> ParticipantFormEntries { get; set; } = new List<ParticipantFormEntry>();

    [ForeignKey("TrialId")]
    [InverseProperty("CaseReportforms")]
    public virtual Trial Trial { get; set; } = null!;
}
