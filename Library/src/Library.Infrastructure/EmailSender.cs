using Library.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            var emailClient = new SmtpClient("localhost");
            var message = new MailMessage
            {

                From = new MailAddress(from),
                Subject = subject,
                Body = body


            };
            message.To.Add(new MailAddress(to));
            try
            {
                //await emailClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while sending email {ex.Message}.");
            }

            _logger.LogWarning($"Sending email to {to} from {from} with subject {subject}.");
        }
    }
}
