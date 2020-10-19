using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace UserManagement.Services
{
    public class EmailService:IDisposable
    {
        private string smtpHost;
        private int smtpPort;
        private bool smtpUseSSL;
        private string smtpUserName;
        private string smtpPassword;
        private SmtpClient client;
        public EmailService()
        {
            smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            smtpUseSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpUseSSL"]);
            smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
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