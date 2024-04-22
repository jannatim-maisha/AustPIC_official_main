using ClubWebsite.Models.Sms;

namespace ClubWebsite.Service.Sms
{
    public interface ISmsService
    {
        Task<dynamic> InsertSMSDB(SendSMSInitModel start, SendSMSResultModel end, string Payer, string Name, string Event);
        Task<SendSMSResultModel> SendSMS(SendSMSInitModel createSms);
    }
}
