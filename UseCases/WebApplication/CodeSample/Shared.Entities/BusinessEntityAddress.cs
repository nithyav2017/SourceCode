using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public class BusinessEntityAddress
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public int AddressID { get; set; }
        public int AddressTypeID { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Person Person { get; set; }
        public Address Address { get; set; }
    }
}
