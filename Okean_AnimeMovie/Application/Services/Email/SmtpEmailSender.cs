using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Okean_AnimeMovie.Application.Services.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public SmtpEmailSender(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = _settings.EnableSsl;
            client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
            client.Timeout = 10000;

            // Ensure From address is acceptable by provider (Gmail requires authenticated address)
            var fromEmail = _settings.From;
            if (string.IsNullOrWhiteSpace(fromEmail) || _settings.Host.Contains("smtp.gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                fromEmail = _settings.UserName;
            }

            var from = new MailAddress(fromEmail, _settings.DisplayName);
            var to = new MailAddress(toEmail);

            using var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
        }
    }
}


