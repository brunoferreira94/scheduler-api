using Scheduler.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Scheduler.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public void SendEmail(string toEmail, string subject, string message)
        {
            using SmtpClient client = new(_smtpServer, _smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            MailMessage mailMessage = new()
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = message,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);

            //client.Send(mailMessage);
            Console.WriteLine(message);
        }
    }
}