using PharmaClinicalSuite.Domain.Models;

namespace PharmaClinicalSuite.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
