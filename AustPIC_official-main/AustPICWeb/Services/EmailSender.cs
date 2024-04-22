using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace AustPICWeb.Services
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration Configuration;

        public EmailSender(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //Read SMTP settings from AppSettings.json.
            var host = Configuration.GetValue<string>("Smtp:Server");
            int port = Configuration.GetValue<int>("Smtp:Port");
            var mail = Configuration.GetValue<string>("Smtp:FromAddress");
            var userName = Configuration.GetValue<string>("Smtp:UserName");
            var pass = Configuration.GetValue<string>("Smtp:Password");

            var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pass)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}