using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

[Table("AuditTrail")]
public partial class AuditTrail
{
    [Key]
    public int AuditTrailId { get; set; }

    [StringLength(128)]
    public string TableName { get; set; } = null!;

    [StringLength(10)]
    public string Action { get; set; } = null!;

    public string KeyValues { get; set; } = null!;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? ChangedColumns { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    public DateTime Timestamp { get; set; }
}
