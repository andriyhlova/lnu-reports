using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpClient _client;
        private readonly IConfigurationProvider _configuration;

        public EmailService(ISmtpClient client, IConfigurationProvider configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task SendEmail(string email, string subject, string htmlBody)
        {
            var smtpHost = _configuration.Get(ConfigNames.SmtpHost);
            var smtpPort = Convert.ToInt32(_configuration.Get(ConfigNames.SmtpPort));
            var smtpUseSSL = Convert.ToBoolean(_configuration.Get(ConfigNames.SmtpUseSSL));
            var smtpUserName = _configuration.Get(ConfigNames.SmtpUserName);
            var smtpPassword = _configuration.Get(ConfigNames.SmtpPassword);

            var message = GetMailMessage(email, subject, htmlBody);
            await _client.ConnectAsync(smtpHost, smtpPort, smtpUseSSL);
            await _client.AuthenticateAsync(smtpUserName, smtpPassword);
            await _client.SendAsync(message);
            await _client.DisconnectAsync(true);
        }

        private MimeMessage GetMailMessage(string email, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            var address = _configuration.Get(ConfigNames.FromEmailAddress);
            var from = new MailboxAddress("LnuReports", address);
            message.From.Add(from);
            var to = new MailboxAddress("User", email);
            message.To.Add(to);
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody;
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
    }
}