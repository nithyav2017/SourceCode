using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PharmaClinicalSuite.Models
{
    public class Participants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParticipantId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [NonFutureDate]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone] 
        public string Phone { get; set; }
        public string Gender { get; set; } = string.Empty;
        public List<SelectListItem> GenderListOption { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; }=string.Empty;
        [Required]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; } = string.Empty;
        public List<SelectListItem>MedicalHistoryOption { get; set; }  
        public string Allergies { get; set; } = string.Empty;
        public float BMI { get;set; }
        public string GuardianInfo { get; set; } = string.Empty; //for minor
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<AdverseEvents> AdverseEvents { get; set; }

    }

    public class NonFutureDateAttribute : ValidationAttribute
    {
        public NonFutureDateAttribute()
        {
            ErrorMessage = "The {0} field can not be a future date.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            if (value is DateTime dateValue)
            {
                return dateValue.Date <= DateTime.Today;
            }
            return false;
        }
    }

}
