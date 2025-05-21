using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class ParticipantFieldDatum
{
    [Key]
    [Column("DataID")]
    public int DataId { get; set; }

    [Column("EntryID")]
    public int EntryId { get; set; }

    [Column("FieldID")]
    public int FieldId { get; set; }

    public string? FieldValue { get; set; }

    [ForeignKey("EntryId")]
    [InverseProperty("ParticipantFieldData")]
    public virtual ParticipantFormEntry Entry { get; set; } = null!;

    [ForeignKey("FieldId")]
    [InverseProperty("ParticipantFieldData")]
    public virtual DataCollectionField Field { get; set; } = null!;
}
