namespace ClubWebsite.Models.Sms
{
    public class SendSMSResultModel
    {

        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public List<SMSResultData> Data { get; set; }
   
    }
    public class SMSResultData
    {
        public int MessageErrorCode { get; set; }
        public string MessageErrorDescription { get; set; }
        public string MobileNumber { get; set; }
        public string MessageId { get; set; }
        public string Custom { get; set; } 
    }
}

