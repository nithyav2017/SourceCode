using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;


        public ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; }

    }
}
