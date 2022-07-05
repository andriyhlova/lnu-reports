using System;
using System.Configuration;
using MailKit.Net.Smtp;
using MimeKit;

namespace SRS.Services.Implementations
{
    public class EmailService : IDisposable
    {
        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly bool smtpUseSSL;
        private readonly string smtpUserName;
        private readonly string smtpPassword;
        private readonly SmtpClient client;

        public EmailService()
        {
            smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            smtpUseSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpUseSSL"]);
            smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            client = new SmtpClient();
        }

        ~EmailService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SendEmail(string email, string subject, string htmlBody)
        {
            // get change password page
            MimeMessage message = GetMailMessage(email, subject, htmlBody);

            client.Connect(smtpHost, smtpPort, smtpUseSSL);

            client.Authenticate(smtpUserName, smtpPassword);

            client.Send(message);
            client.Disconnect(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            client?.Dispose();
        }

        private MimeMessage GetMailMessage(string email, string subject, string htmlBody)
        {
            // get change password page
            MimeMessage message = new MimeMessage();

            string address = "no-reply@lnu.edu.ua";

            MailboxAddress from = new MailboxAddress("LnuReports", address);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", email);
            message.To.Add(to);

            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();

            // generate body
            bodyBuilder.HtmlBody = htmlBody;

            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
    }
}