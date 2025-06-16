using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static PharmaClinicalSuite.Models.Data_Collection;
using System.Collections.Generic;

namespace PharmaClinicalSuite.Models
{
    public class Sites
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SiteId { get; set; } 
        public string SiteName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ContactInformation { get;set; } = string.Empty;

      // public ICollection<TrialSite> TrialSites { get; set; }
        public ICollection<Trials> Trials { get; set; }
        
    }
    public class TrialSite
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialSiteId { get; set; }

        [ForeignKey("Trials")]
        public int TrialTypeId { get; set; }       
        public Trials Trial { get; set; }

        [ForeignKey("Sites")]
        public int SiteId { get; set; }
       // public ICollection<Sites> Sites { get; set; }
    }


}
