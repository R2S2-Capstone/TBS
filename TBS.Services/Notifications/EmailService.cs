using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Notifications;

namespace TBS.Services.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toName, string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(name: _configuration["SMTP:Name"], address: _configuration["SMTP:Email"]));
            email.To.Add(new MailboxAddress(name: toName, address: toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.ConnectAsync(_configuration["SMTP:Host"], 587, false)
                    .ConfigureAwait(false);
                await client.AuthenticateAsync(_configuration["SMTP:Username"], _configuration["SMTP:Password"])
                    .ConfigureAwait(false);

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

            return await Task.FromResult(true);
        }
    }
}
