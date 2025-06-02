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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } = DateTime.Today; 
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone] 
        public string Phone { get; set; }
        public string Gender { get; set; } = string.Empty;
        [NotMapped ]
        public List<SelectListItem>? GenderListOption { get; set; } = null;
        [Required]
        public string Address1 { get; set; } = string.Empty;
        [Required]
        public string City { get; set; }=string.Empty;
        //public List<SelectListItem> CityOption { get; set; }

        //[Required]
        //public string State { get; set; }
        //public List<SelectListItem> StateOption { get; set; }

        //[Required]
        //public string Country { get; set; }
        //public List<SelectListItem> CountryOption { get; set; }

        [Required]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; } = string.Empty;
        [NotMapped]
        public List<SelectListItem>? MedicalHistoryOption { get; set; } = null;
        public string Allergies { get; set; } = string.Empty;
        public decimal BMI { get;set; }
        public string GuardianInfo { get; set; } = string.Empty; //for minor
        public ICollection<Enrollment>? Enrollments { get; set; } = null;
        public ICollection<AdverseEvents>? AdverseEvents { get; set; } = null;

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
