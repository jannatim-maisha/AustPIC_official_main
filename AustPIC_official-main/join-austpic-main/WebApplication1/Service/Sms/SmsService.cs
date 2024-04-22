using ClubWebsite.DBContexts;
using ClubWebsite.Models.Common;
using ClubWebsite.Models.Join;
using ClubWebsite.Models.Payment;
using ClubWebsite.Models.Sms;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;
namespace ClubWebsite.Service.Sms
{
    public class SmsService : ISmsService
    {
        
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IDapperDBContext _dapperDBContext;
        private String InsertSMSDBSP = "ClubWebsite_InsertSms";
        #endregion

        #region Ctor

        public SmsService(IConfiguration configuration,IDapperDBContext dapperDBContext)
        {
            _configuration = configuration;
            _dapperDBContext = dapperDBContext; 
        }

        #endregion

        #region Methods
        
        public async Task<SendSMSResultModel> SendSMS(SendSMSInitModel createSms)
        {
            if (createSms == null || createSms.MobileNumbers==null)
                return new SendSMSResultModel();
            if (createSms.SecretKey != _configuration.GetSection("SMSQ")["SecretKey"])
                return new SendSMSResultModel();
            string url = _configuration.GetSection("SMSQ")["CreateSMSGetURL"] + "?SenderId=" + _configuration.GetSection("SMSQ")["SenderId"] + "&Is_Unicode=" + createSms.Is_Unicode + "&Is_Flash=" + createSms.Is_Flash + "&Message=" + createSms.Message + "&MobileNumbers=" + createSms.MobileNumbers + "&ApiKey=" + _configuration.GetSection("SMSQ")["ApiKey"] + "&ClientId=" + _configuration.GetSection("SMSQ")["ClientId"];
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            SendSMSResultModel result = new SendSMSResultModel();
            if (response != null)
            {
                result = JsonSerializer.Deserialize<SendSMSResultModel>(response.Content);
            }
            return result;
        }
        public async Task<dynamic> InsertSMSDB(SendSMSInitModel start,SendSMSResultModel end, string Payer,string Name,string Event)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<dynamic>(new
                {
                    PAYER = Payer,
                    NAME = Name,
                    SENTTIME = DateTime.UtcNow.AddHours(6),
                    MOBILENUMBER = start.MobileNumbers,
                    MESSAGEBODY = start.Message,
                    ISUNICODE = start.Is_Unicode,
                    ISFLASH = start.Is_Flash,
                    SECRETKEY = start.SecretKey,
                    MESSAGEID = end.Data[0].MessageId,
                    ERRORCODE = end.ErrorCode,
                    ERRORDESCRIPTION = end.ErrorDescription,
                    MESSAGEERRORCODE = end.Data[0].MessageErrorCode,
                    MESSAGEERRORDESCRIPTION = end.Data[0].MessageErrorDescription,
                    CUSTOM = end.Data[0].Custom,
                    EVENT = Event
                }, InsertSMSDBSP) ;
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }



        #endregion
    }
}
