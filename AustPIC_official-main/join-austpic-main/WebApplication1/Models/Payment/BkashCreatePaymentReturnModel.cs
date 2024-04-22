namespace ClubWebsite.Models.Payment
{
    public class BkashCreatePaymentReturnModel
    {
        public string paymentID { get; set; }
        public string createTime { get; set; }
        public string orgLogo { get; set; }
        public string orgName { get; set; }
        public string transactionStatus { get; set; }
        public string amount  { get; set; }
        public string currency { get; set; }

        public string intent { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
