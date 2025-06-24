using PharmaClinicalSuite.Domain.Interfaces;
using PharmaClinicalSuite.Domain.Models;
using System.Net;
using System.Net.Mail;

namespace PharmaClinicalSuite.Services
{
    public class SmtpEmailService : IEmailService
    {
        public readonly IConfiguration _config;

        public SmtpEmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            using var smtp = new SmtpClient
            {
                Host = _config["Email:SmtpHost"],
                Port = int.Parse(_config["Email:Port"]),
                Credentials = new NetworkCredential(
                 _config["Email:Username"],
                 _config["Email:Password"]),
                EnableSsl = true
            };
            var mail = new MailMessage("nithya.jganesh@gmail.com", emailMessage.To, emailMessage.Subject, emailMessage.Body);
            await smtp.SendMailAsync(mail);
        }
    }
}
