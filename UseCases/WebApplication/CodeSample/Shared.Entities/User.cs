using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class Person
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        [Column("EmailAddress")]
         public string Email { get; set; }
        [NotMapped]
         public string PhoneNumber { get; set; }

        [NotMapped]
        public Address Address { get; set; }



        //  Navigation properties
         public ICollection<EmailAddress> EmailAddresses { get; set; }
         public ICollection<PersonPhone> Phones { get; set; }
         public ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
    }


}

