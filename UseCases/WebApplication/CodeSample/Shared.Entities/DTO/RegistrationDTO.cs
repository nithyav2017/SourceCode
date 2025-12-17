using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Shared.Entities.DTO
{
    public class RegistrationDTO
    {
       // [Key]
       // public int BusinessEntityID { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? EmailAddress { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
