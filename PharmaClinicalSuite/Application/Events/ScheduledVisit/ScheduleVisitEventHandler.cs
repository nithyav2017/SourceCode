using MediatR;
using PharmaClinicalSuite.Domain.Interfaces;
using PharmaClinicalSuite.Domain.Models;
using PharmaClinicalSuite.Models.Events;

namespace PharmaClinicalSuite.Application.Events.ScheduledVisit
{
    public class ScheduleVisitEventHandler : INotificationHandler<VisitScheduledEvent>
    {
        private readonly IEmailService _emailService;

        public ScheduleVisitEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Handle(VisitScheduledEvent notification, CancellationToken cancellationToken)
        {
            var email = new EmailMessage
            {
                To = "nithya.venkatakrishnan2017@gmail.com",
                Subject = "Confirmation",
                Body = $"Visit #{notification.VisitId} for Participant #{notification.ParticipantId} has been scheduled."
            };

            await _emailService.SendEmailAsync(email);
        }
    }
}
