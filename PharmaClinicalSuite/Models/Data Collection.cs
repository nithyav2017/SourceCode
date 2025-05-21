using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Models
{
    public class Data_Collection
    {
        public class CaseReportform
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CRFID { get; set; }  
            [ForeignKey("Trials")]
            public int TrialId { get; set; }
            public Trials Trails { get; set; }
            public string FormName { get; set; }
            public string Description { get; set; } = string.Empty;
            public DateTime CreatedOn { get; set; }
        }

    
        public class DataCollectionFields
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int FieldId { get;set; }
            public CaseReportform CRFID { get;set; }
            public string FieldName { get; set; } = string.Empty;
            public string FieldType { get; set; } = string.Empty;
            public bool IsRequired { get;set; } 
            public int FieldOrder { get; set; }
        }
        public class CRFData
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DataId { get; set;  } 
            public Participants ParticipantId { get; set; }
            public DataCollectionFields FieldId { get; set; }
            public string FieldValue { get; set; } = string.Empty;
          //  public DateTime EntryDate { get;set;}

        }
    }
}
