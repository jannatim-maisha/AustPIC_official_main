using ClubWebsite.Models.Email;
using ClubWebsite.Service.Email;
using ClubWebsite.Service.Join;
using Microsoft.AspNetCore.Mvc;

namespace ClubWebsite.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
       public EmailController(IEmailService emailService,IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }
        private async Task<IActionResult> Index()
        {
            SendEmailInitModel emailModel = new SendEmailInitModel();
            emailModel.FromEmail = _configuration.GetSection("MailSettings")["FromEmail"];
            emailModel.Body = "Test";
            emailModel.ReplyTo = _configuration.GetSection("MailSettings")["ReplyTo"];
            emailModel.Subject = "Registration Confirmation for AUST Programming and Informatics Club (AUSTPIC)";
            emailModel.ToEmails = new List<string>();
            emailModel.ToEmails.Add("bondhuhinalam@gmail.com");
            emailModel.PassWord = _configuration.GetSection("MailSettings")["Password"];
            emailModel.SendDate = DateTime.UtcNow.AddHours(6);
            emailModel.Attachments = new List<IFormFile>();
            emailModel.Bcc = new List<string>();
            emailModel.Cc = new List<string>();
            var res = await _emailService.SendEmailAsync(emailModel, "Test");
            return View();
        }
    }
}
