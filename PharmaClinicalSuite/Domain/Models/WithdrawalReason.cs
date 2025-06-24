using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Domain.Models
{
    public class WithdrawalReason
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WithdrawalReasonID { get; set; }
        public string WithdrawalReasonDesc { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
