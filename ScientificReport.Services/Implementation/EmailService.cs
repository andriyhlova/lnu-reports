using System;
using System.Configuration;
using MimeKit;
using ScientificReport.Services.Abstraction;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ScientificReport.Services.Implementation
{
    public class EmailService: IEmailService, IDisposable
    {
        private string smtpHost;
        private int smtpPort;
        private bool smtpUseSSL;
        private string smtpUserName;
        private string smtpPassword;
        private SmtpClient client;
        public EmailService(string smtpHost, string smtpPort, string smtpUserName, string smtpPassword, string smtpUseSsl)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = Convert.ToInt32(smtpPort);
            this.smtpUserName = smtpUserName;
            this.smtpPassword = smtpPassword;
            this.smtpUseSSL = Convert.ToBoolean(smtpUseSsl);

            client = new SmtpClient();
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
            //bodyBuilder.HtmlBody = body;
            bodyBuilder.HtmlBody = htmlBody;

            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
    }
}