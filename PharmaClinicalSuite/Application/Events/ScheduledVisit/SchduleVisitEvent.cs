using MediatR;

namespace PharmaClinicalSuite.Application.Events.ScheduledVisit
{
    public class SchduleVisitEvent:INotification
    {
        public int ParticipantId { get; }
        public int VisitId { get; }

        public SchduleVisitEvent(int participantId, int visitId)
        {
            ParticipantId = participantId;
            VisitId = visitId;
        }
    }
}
