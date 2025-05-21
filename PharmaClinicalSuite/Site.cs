using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite;

public partial class Site
{
    [Key]
    [Column("SiteID")]
    public int SiteId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SiteName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Location { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactInformation { get; set; }

    [ForeignKey("SiteId")]
    [InverseProperty("Sites")]
    public virtual ICollection<Trial> Trials { get; set; } = new List<Trial>();
}
