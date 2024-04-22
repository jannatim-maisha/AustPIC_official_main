namespace ClubWebsite.Models.Sms
{
    public class SendSMSInitModel
    {
        public string Message { get; set; }
        public string MobileNumbers { get; set; }
        public bool Is_Unicode { get; set; }
        public bool Is_Flash { get; set; }
        public string SecretKey { get; set; }

    }
}
