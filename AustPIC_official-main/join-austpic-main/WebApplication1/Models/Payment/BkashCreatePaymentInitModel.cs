namespace ClubWebsite.Models.Payment
{
    public class BkashCreatePaymentInitModel
    {
        public string currency { get; set; }
        public string amount { get; set; }
        public string intent { get; set; }
        public string invoiceNumber { get; set; }

    }
}
