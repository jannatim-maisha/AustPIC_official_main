using ClubWebsite.Models.Payment;

namespace ClubWebsite.Service.Payment
{
    public interface IPaymentService
    {
        Task<BkashCreatePaymentReturnModel> CreatePayment(BkashCreatePaymentInitModel createModel);
        Task<BkashExecutePaymentReturnModel> ExecutePayment(BkashExecutePaymentInitModel createModel);
        Task<string> GenerateInvoiceNumber();
        Task<dynamic> InsertPaymentDB(BkashExecutePaymentReturnModel model, string Payer, string Event, string Name, string Email, string Phone, string Address);
        Task<dynamic> InsertTransactionDB(BkashSearchTransactionReturnModel searchTransaction);
        Task<int> ModifyInvoiceStatus(string InvoiceNo, int Status);
        Task<BkashQueryPaymentReturnModel> QueryPayment(string paymentID);
        Task<BkashSearchTransactionReturnModel> SearchTransaction(string trxID);
    }
}
