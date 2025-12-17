using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class EmailAddress
    {
        [Key, ForeignKey("Person")]
       public int BusinessEntityID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailAddressID { get; set; }
        [Column("EmailAddress")]
        public string Email    { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;


        // Navigation
        public Person Person { get; set; }



    }
}
