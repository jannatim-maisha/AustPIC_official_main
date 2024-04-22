namespace ClubWebsite.Models.Payment
{
    public class BkashQueryPaymentReturnModel
    {
        public string paymentID { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }
        public string trxID { get; set; }
        public string transactionStatus { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string refundAmount { get; set; }
        public string customerMsisdn { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
