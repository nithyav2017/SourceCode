using MediatR;
using PharmaClinicalSuite.Models;
using PharmaClinicalSuite.Models.Interfaces;

namespace PharmaClinicalSuite.Models.Events
{
    public class VisitScheduledEvent : IDomainEvent, INotification
    {
        public int ParticipantId { get; set; }
        public int VisitId { get; set; }
        public DateTime OccuredOn { get; }
        public string Notes { get; }
        public string status { get; }

        public VisitScheduledEvent(int participantId, int visitId,DateTime scheduledDate,string notes,string status)
        {
            ParticipantId = participantId;
            VisitId = visitId;
            OccuredOn = scheduledDate;
            this.Notes = notes;
            this.status = status;
           
        }
    
    }
}
