using AutoMapper.Internal;
using ClubWebsite.Models.Email;
using MailKit.Security;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using ClubWebsite.Models.Payment;
using ClubWebsite.Models.Sms;
using ClubWebsite.DBContexts;

namespace ClubWebsite.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IDapperDBContext _dapperDBContext;
        String InsertEmailDBSP = "ClubWebsite_InsertEmail";
        public EmailService(IConfiguration configuration,IDapperDBContext dapperDBContext)
        {
            _configuration = configuration;
            _dapperDBContext = dapperDBContext;
        }

        public async Task<dynamic> InsertEmailDB(SendEmailInitModel model, string Event,string messsage)
        {
            string ToEmail = "";
            foreach (var item in model.ToEmails)
            {
                if (ToEmail == "")
                {
                    ToEmail = item;
                }
                else ToEmail += "," + item;
            }
            string Cc = "";
            foreach (var item in model.Cc)
            {
                if (Cc == "")
                {
                    Cc = item;
                }
                else Cc += "," + item;
            }
            string Bcc = "";
            foreach (var item in model.Bcc)
            {
                if (Bcc == "")
                {
                    Bcc = item;
                }
                else Bcc += "," + item;
            }
            string status = "";
            if(messsage.Contains("2.0.0 OK"))
            {
                status = "Sent";
            }
            else
            {
                status = "Not Sent";
            }
            string Attachments = "";
            foreach(var item in model.Attachments)
            {
                if (Attachments == "")
                {
                    Attachments = item.FileName;
                }
                else Attachments += "," + item.FileName;
            }

            try
            {
                var result = await _dapperDBContext.GetInfoAsync<dynamic>(new
                {
                    SENTTIME = model.SendDate,
                    FROMEMAIL  = model.FromEmail,
                    TOEMAIL = ToEmail,
                    EMAILBODY = model.Body,
                    CC = Cc,
                    BCC = Bcc,
                    STATUS = status,
                    ATTACHMENTS = Attachments,
                    REPLYTO = model.ReplyTo,
                    EVENT = Event,
                    INSERTTIME = DateTime.UtcNow.AddHours(6),
                    SUBJECT = model.Subject
                }, InsertEmailDBSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<string> SendEmailAsync(SendEmailInitModel sendEmail,string Event)
        {
            if(sendEmail==null)
            {
                return "";
            }
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(sendEmail.FromEmail);
            for (int i = 0; i < sendEmail.ToEmails?.Count; i++)
            {
                email.To.Add(MailboxAddress.Parse(sendEmail.ToEmails[i]));
            }
            for (int i=0;i< sendEmail.Cc?.Count;i++)
            {
                email.Cc.Add(MailboxAddress.Parse(sendEmail.Cc[i]));
            }
            for (int i = 0; i < sendEmail.Bcc?.Count; i++)
            {
                email.Bcc.Add(MailboxAddress.Parse(sendEmail.Bcc[i]));
            }
            email.Subject = sendEmail.Subject;
            email.InReplyTo = sendEmail.FromEmail;
            
            var builder = new BodyBuilder();
            if (sendEmail.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in sendEmail.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = sendEmail.Body;
            email.Body = builder.ToMessageBody();
            var smtp = new SmtpClient();
            string res = "";
            try
            {
                smtp.Connect(_configuration.GetSection("MailSettings")["Host"], Int32.Parse(_configuration.GetSection("MailSettings")["Port"]), SecureSocketOptions.StartTls);
                smtp.Authenticate(sendEmail.FromEmail,sendEmail.PassWord);
                smtp.Timeout = 5000;
                res = await smtp.SendAsync(email);
            }
            catch(Exception e)
            {
                res = "Exception\n"+e.Message.ToString();
            }
            smtp.Disconnect(true);
            await InsertEmailDB(sendEmail, Event, res);
            return res;
        }
    }
}
