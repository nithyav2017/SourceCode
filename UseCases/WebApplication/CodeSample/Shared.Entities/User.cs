using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class Person
    { 
        [Key ,ForeignKey("BusinessEntity")] 
        public int BusinessEntityID { get; set; }
        public BusinessEntity BusinessEntity{ get; set; }

        public string PersonType { get; set; } = "EM";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        [Column("EmailAddress")]
        public EmailAddress Email { get; set; }
        [NotMapped]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public Address Address { get; set; }

        public Password Password { get; set; }

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;





        //  Navigation properties
        public ICollection<EmailAddress> EmailAddresses { get; set; }
         public ICollection<PersonPhone> Phones { get; set; }
         public ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
    }


}

