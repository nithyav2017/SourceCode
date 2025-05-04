using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientInfo.Models;
using System.ComponentModel.DataAnnotations;

namespace PatientInfo.Data
{
    public class ApplicationUser : IdentityUser
    {  
        public string Id { get; set; }
        public string Email { get; set; }         
        public string PasswordHash { get; set; }
        public string PINHash { get; set; }
        public PatientDetails PatientDetails { get; set; }
    }
}
