using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public class PersonPhone
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberTypeID { get; set; }
      
        public DateTime ModifiedDate { get; set; }

        public Person Person { get; set; }
    }
}
