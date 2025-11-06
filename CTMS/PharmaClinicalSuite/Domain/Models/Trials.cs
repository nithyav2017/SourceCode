using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using static PharmaClinicalSuite.Domain.Models.Data_Collection;

namespace PharmaClinicalSuite.Domain.Models
{
    public class Trials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialId { get; set; }
        public string title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Phase { get; set; }
        public string Sponsor { get; set; }
       
        [ForeignKey("TrialType")]
        public int TrialTypeId { get; set; }
        public TrialType TrialsType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<AdverseEvents> AdverseEvents { get; set; }
        public ICollection<CaseReportform> CaseReportForms { get; set; }
        public ICollection<TrialSite> TrialSites { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Sites> Sites { get; set; }
        public ICollection<TrialInvestigator> TrialsInvestigators { get; set; }
    }

    public class TrialType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialTypeId { get; set; }  
        public string TypeName { get; set; } = string.Empty;
        public string Phase { get;set; } = string.Empty;
        public ICollection<Trials> Trials { get; set; }


    }
}
