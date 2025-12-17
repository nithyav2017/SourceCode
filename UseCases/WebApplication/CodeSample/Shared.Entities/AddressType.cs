using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Entities
{
    public  class AddressType
    {
        [Key]
        public int AddressTypeID { get; set; }
        public string Name { get; set; } 
        public Guid rowguid { get; set; }= Guid.NewGuid();
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
