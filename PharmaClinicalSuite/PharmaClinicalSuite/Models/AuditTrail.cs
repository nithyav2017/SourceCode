using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaClinicalSuite.Models
{
    public class AuditTrail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditTrailId { get; set; }  
        public string UserId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;  
        public string TableName { get; set; } = string.Empty;
        public string RecordId { get; set; } = string.Empty;
        public TimeOnly Timestamp { get; set; }
        public string ChangeDetails { get; set; } = string.Empty;   
    }
}
