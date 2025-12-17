using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Entities
{
    public class BusinessEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessEntityID { get; set; }
        public Guid rowguid { get; set; }
        
        public Person Person { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;


    }
}
