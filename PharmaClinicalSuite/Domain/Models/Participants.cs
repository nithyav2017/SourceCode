using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmaClinicalSuite.Models.Events;
using PharmaClinicalSuite.Utility;

namespace PharmaClinicalSuite.Domain.Models
{
    public class Participants
    {
         public List<Visit> Visits { get; private set; } = new List<Visit>();

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
        [NotMapped]
        public List<SelectListItem>? GenderListOption { get; set; } = null;
        [Required]
        public string Address1 { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
       
        [Required]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; } = string.Empty;
        [NotMapped]
        public List<SelectListItem>? MedicalHistoryOption { get; set; } = null;
        public string Allergies { get; set; } = string.Empty;
        public decimal BMI { get; set; }
        public string GuardianInfo { get; set; } = string.Empty; //for minor
        public ICollection<Enrollment>? Enrollments { get; set; } = null;
        public ICollection<AdverseEvents>? AdverseEvents { get; set; } = null;
       // public ICollection<Visit> visits { get; set; } = null;

        [NotMapped]
        public int SimilarityScore { get; set; }

    


    public Result ScheduleVisit(int participantId, DateTime scheduledDate, string visitType, string notes,string status)
        {
           // Sites sites = new Sites();
          //  if (!sites.IsActive) return Result.Failure("Site is inactive.");
          //  if (!IsEligibleForVisit(visitType)) return Result.Failure("Invalid visit type.");

            var visit = new Visit(participantId, scheduledDate, visitType,notes,status);
            Visits.Add(visit);

            // Raise domain event
            DomainEvents.Raise(new VisitScheduledEvent(participantId, visit.Id,visit.ScheduledDate,visit.Notes,visit.Status));

            return Result.Success();
        }

        private bool IsEligibleForVisit(string visitType) => true; // Simplified

    }
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

  