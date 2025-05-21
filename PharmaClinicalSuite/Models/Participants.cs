using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaClinicalSuite.Models
{
    public class Participants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParticipantId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        [Phone] public string Phone { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; }=string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public float BMI { get;set; }
        public string GuardianInfo { get; set; } = string.Empty; //for minor
        public ICollection<Enrollment> Enrollments { get; set; }


    }
}
