using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Domain.Models
{
    public class Data_Collection
    {
        public class CaseReportform
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int FormId { get; set; }  
            [ForeignKey("Trials")]
            public int TrialId { get; set; }
            public Trials Trials { get; set; }
            public string FormName { get; set; }
            public string Description { get; set; } = string.Empty;
            public DateTime CreatedOn { get; set; }
            public ICollection<DataCollectionFields> DataCollectionFields { get; set; }            
            public virtual ICollection<ParticipantFormEntries> ParticipantFormEntries { get; set; } 

        }


        public class DataCollectionFields
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int FieldId { get;set; }
            [ForeignKey("CaseReportform")]
            public int FormId { get;set; }
            public CaseReportform Forms { get; set; }
            public string FieldName { get; set; } = string.Empty;
            public string FieldType { get; set; } = string.Empty;
            public bool IsRequired { get;set; } 
            public int FieldOrder { get; set; }
            public ICollection<ParticipantFieldData> ParticipantFieldData {  get; set; }
         
        }
        public class ParticipantFieldData
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DataId { get; set;  }

            [ForeignKey("ParticipantFormEntries")]
            public int EntryId { get; set; }
            public virtual  ParticipantFormEntries ParticipantFormEntries { get; set; }

            [ForeignKey("DataCollectionFields")]
            public int FieldId { get; set; }
            public virtual DataCollectionFields DataCollectionFields { get; set; }
          


            public string FieldValue { get; set; } = string.Empty;
          
            //  public DateTime EntryDate { get;set;}

        }

        public class ParticipantFormEntries
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int EntryId { get;set; }

       //     [ForeignKey("ParticipantFieldData")]
        //    public int ParticipantID { get; set; }
            public virtual  ICollection<ParticipantFieldData> ParticipantFieldData { get; set; }

            [ForeignKey("CaseReportform")]
            public int FormID { get; set; }
            public virtual CaseReportform CaseReportforms { get; set; }
            public DateTime EntryDate { get; set; }
        }
    }
}
