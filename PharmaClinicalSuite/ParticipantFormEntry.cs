using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class ParticipantFormEntry
{
    [Key]
    [Column("EntryID")]
    public int EntryId { get; set; }

    [Column("ParticipantID")]
    public int ParticipantId { get; set; }

    [Column("FormID")]
    public int FormId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [ForeignKey("FormId")]
    [InverseProperty("ParticipantFormEntries")]
    public virtual CaseReportform Form { get; set; } = null!;

    [ForeignKey("ParticipantId")]
    [InverseProperty("ParticipantFormEntries")]
    public virtual Participant Participant { get; set; } = null!;

    [InverseProperty("Entry")]
    public virtual ICollection<ParticipantFieldDatum> ParticipantFieldData { get; set; } = new List<ParticipantFieldDatum>();
}
