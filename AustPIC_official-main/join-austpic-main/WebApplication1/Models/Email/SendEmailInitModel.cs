using System.Globalization;

namespace ClubWebsite.Models.Email
{
    public class SendEmailInitModel
    {
        public List<string> ToEmails { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string ReplyTo { get; set; }
        public string PassWord { get; set; }
        public List<IFormFile> Attachments { get; set; }


    }
}
