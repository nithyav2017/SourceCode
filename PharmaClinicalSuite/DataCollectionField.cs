using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class DataCollectionField
{
    [Key]
    [Column("FieldID")]
    public int FieldId { get; set; }

    [Column("FormID")]
    public int FormId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FieldName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? FieldType { get; set; }

    public bool? IsRequired { get; set; }

    public int? FieldOrder { get; set; }

    [ForeignKey("FormId")]
    [InverseProperty("DataCollectionFields")]
    public virtual CaseReportform Form { get; set; } = null!;

    [InverseProperty("Field")]
    public virtual ICollection<ParticipantFieldDatum> ParticipantFieldData { get; set; } = new List<ParticipantFieldDatum>();
}
