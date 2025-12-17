using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Entities
{
    public class Registration
    {
        [Key]
        public int BusinessEntityID { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public EmailAddress EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public Password password { get; set; } = null!;

    }
}
