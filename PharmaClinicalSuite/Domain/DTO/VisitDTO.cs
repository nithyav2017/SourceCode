namespace PharmaClinicalSuite.Domain.DTO
{
    public class VisitDTO
    {
        public string ParticipantName { get; set; } 
        public string Notes { get; set; }
        public string VisitType { get; set; }
        public DateTime ScheduledDate { get; set; }

    }
}
