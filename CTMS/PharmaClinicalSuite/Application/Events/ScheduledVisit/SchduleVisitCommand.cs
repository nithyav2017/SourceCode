using MediatR;
using Microsoft.Build.ObjectModelRemoting;
using NuGet.Protocol.Plugins;
using PharmaClinicalSuite.Utility;

namespace PharmaClinicalSuite.Application.Events.ScheduledVisit
{
    public class SchduleVisitCommand : IRequest<Result>
    {
        public int ParticipantId { get; set; }
        public DateTime SchduleDate { get; set; }
        public string VisitType { get; set; }
        public string Notes { get; set; }

    }
}
