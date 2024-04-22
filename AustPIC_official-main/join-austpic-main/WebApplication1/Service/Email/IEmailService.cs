using ClubWebsite.Models.Email;

namespace ClubWebsite.Service.Email
{
    public interface IEmailService
    {
        Task<dynamic> InsertEmailDB(SendEmailInitModel model, string Event, string messsage);
        Task<string> SendEmailAsync(SendEmailInitModel sendEmail, string Event);
    }
}
