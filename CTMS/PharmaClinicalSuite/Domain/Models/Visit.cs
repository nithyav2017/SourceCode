using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace PharmaClinicalSuite.Domain.Models
{
    [Table("Visit")]
    public class Visit
    {
        
        public Visit(int participantId, DateTime ScheduledDate, string VisitType,string notes,string status)
        {
            this.ParticipantId = participantId;
            this.ScheduledDate = ScheduledDate;
            this.VisitType = VisitType;
            this.Notes = notes;
            this.Status = "Scheduled";
        }

        public Visit()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }
        public DateTime ScheduledDate { get; set; } = DateTime.Today;
        public DateTime? ActualVisitDate { get; set; } = null;
        public string VisitType { get; set; } // Screening, Follow-up, Final, etc.
        [NotMapped]
        public List<SelectListItem>? VisitTypeOption { get; set; } = new()
        {
            new SelectListItem { Text = "Initial Visit", Value = "Initial" },
            new SelectListItem { Text = "Follow-Up", Value = "FollowUp" },
            new SelectListItem{Text="Emergency", Value="Emergency"}
        };
        public string Status { get; set; } // Scheduled, Completed, Missed

        [NotMapped]
        public List<SelectListItem>? StatusOption { get; set; } = new()
        {
            new SelectListItem{Text="Scheduled", Value="Scheduled"},
            new SelectListItem{Text="Completed", Value ="Completed"},
            new SelectListItem{Text ="Missed", Value="missed"},
            new SelectListItem {Text="Rescheduled", Value="Rescheduled"}
        };
        public string Notes { get; set; }

        public  Participants Participant { get; set; } // Navigation property
    }
      
}
