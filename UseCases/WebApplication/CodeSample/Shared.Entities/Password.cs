using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Entities
{
    public class Password
    {
        [Key, ForeignKey("Person")]         
        public int BusinessEntityID { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public Person Person { get; set; }
    }
}
