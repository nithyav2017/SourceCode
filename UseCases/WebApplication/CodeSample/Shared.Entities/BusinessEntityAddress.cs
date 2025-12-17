using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class BusinessEntityAddress
    {
        [Key, ForeignKey("BusinessEntity")]
        public int BusinessEntityID { get; set; } 
        public BusinessEntity BusinessEntity { get; set; }
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public Address Address { get; set; }
        [ForeignKey("AddressType")]
        public int AddressTypeID { get; set; }
        public AddressType AddressType { get; set; }
        public Guid RowGuid { get; set; } = Guid.NewGuid();
        public Person Person { get; set; }      
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
