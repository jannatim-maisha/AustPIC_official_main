using AutoMapper;
using AutoMapper.Execution;
using ClubWebsite.DBContexts;
using ClubWebsite.Models.Common;
using ClubWebsite.Models.Join;
using ClubWebsite.Models.Payment;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Globalization;
using System.Numerics;
using System.Text.Json;
using System.Transactions;

namespace ClubWebsite.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        #region Fields

        private readonly IDapperDBContext _dapperDBContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        String GetBkashCredentialsSP = "Payment_GetBkashCredentials";
        String UpdateBkashTokenSP = "Payment_UpdateBkashToken";
        String ModifyInvoiceStatusSP = "Payment_InvoiceStatus";
        String GenerateInvoiceNumberSP = "Payment_GenerateInvoiceNumber";
        String InsertTransactionDBSP = "ClubWebsite_InsertTransaction";
        String InsertPaymentDBSP = "ClubWebsite_InsertPayment";
        #endregion

        #region Ctor

        public PaymentService(IDapperDBContext dapperDBContext, IConfiguration configuration,IMapper mapper)
        {
            _dapperDBContext = dapperDBContext;
            _configuration = configuration;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        private async Task<BkashCredentials> GrantBkashPGWToken()
        {
            BkashCredentials bkash = await GetBkashCredentials();
            var options = new RestClientOptions(_configuration.GetSection("Bkash")["GrantTokenUrl"])
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000  // 30second
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("accept", "application/json");
            request.AddHeader("username", bkash.UserName);
            request.AddHeader("password", bkash.Password);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"app_key\":" + bkash.AppKey + ",\"app_secret\":" + bkash.AppSecret + "}", ParameterType.RequestBody);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response != null && response.IsSuccessful)
                {
                    BkashGrantTokenModel result = new BkashGrantTokenModel();
                    result = JsonSerializer.Deserialize<BkashGrantTokenModel>(response.Content);
                    bkash.RefreshToken = result.refresh_token;
                    bkash.Token = result.id_token;
                    bkash.CreatedOn = DateTime.UtcNow.AddHours(6);
                    bkash.ValiditySeconds = result.expires_in;
                    await UpdateBkashTokenDB(bkash);

                }
            }
            catch(Exception e)
            {
                
            }
            return bkash;
        }
        private async Task<BkashCredentials> RefreshBkashPGWToken(BkashCredentials bkash)
        {
            var options = new RestClientOptions(_configuration.GetSection("Bkash")["RefreshTokenUrl"])
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000  // 30second
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("accept", "application/json");
            request.AddHeader("username", bkash.UserName);
            request.AddHeader("password", bkash.Password);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"app_key\":" + bkash.AppKey + ",\"app_secret\":" + bkash.AppSecret + ",\"refresh_token\":" + bkash.RefreshToken + "}", ParameterType.RequestBody);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response != null && response.IsSuccessful)
                {
                    BkashGrantTokenModel result = new BkashGrantTokenModel();
                    result = JsonSerializer.Deserialize<BkashGrantTokenModel>(response.Content);
                    bkash.RefreshToken = result.refresh_token;
                    bkash.Token = result.id_token;
                    bkash.CreatedOn = DateTime.Now;
                    bkash.ValiditySeconds = result.expires_in;
                    await UpdateBkashTokenDB(bkash);
                }
                else
                {
                    bkash = await GrantBkashPGWToken();
                }
            }
            catch(Exception e)
            {
                await GrantBkashPGWToken();
            }
            return bkash;
        }
        private async Task<BkashCredentials> GetBkashToken()
        {
            BkashCredentials bkash = await GetBkashCredentials();
            DateTime curDateTime = DateTime.UtcNow.AddHours(6);
            TimeSpan diff = (TimeSpan)(curDateTime - bkash.CreatedOn);
            if ((decimal)diff.TotalSeconds - 500 < bkash.ValiditySeconds)
            {
                return bkash;
            }
            else if ((decimal)diff.TotalSeconds < 24 * 3600 * 21)
            {
                return await RefreshBkashPGWToken(bkash);
            }
            return await GrantBkashPGWToken();

        }

        private async Task<BkashCredentials> GetBkashCredentials()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<BkashCredentials>(new
                {
                    Environment = _configuration.GetSection("Bkash")["Environment"],
                }, GetBkashCredentialsSP);
                BkashCredentials bkash = (BkashCredentials)result;
                return bkash;
            }
            catch (Exception ex)
            {
                return new BkashCredentials();
            }
        }
        private async Task<string> UpdateBkashTokenDB(BkashCredentials bkash)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    Token  = bkash.Token,
                    RefreshToken = bkash.RefreshToken,
                    CreatedOn = bkash.CreatedOn,
                    ValiditySeconds = bkash.ValiditySeconds,
                    Environment = _configuration.GetSection("Bkash")["Environment"],
                }, UpdateBkashTokenSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<BkashCreatePaymentReturnModel> CreatePayment(BkashCreatePaymentInitModel createModel)
        {
            BkashCreatePaymentReturnModel returnModel = new BkashCreatePaymentReturnModel();
            if (createModel == null)
                return returnModel;
            BkashCredentials bkash = await GetBkashToken();
            var options = new RestClientOptions(_configuration.GetSection("Bkash")["CreatePaymentUrl"])
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000  // 30second
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bkash.Token);
            request.AddHeader("X-APP-Key", bkash.AppKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"amount\":" + createModel.amount + ",\"currency\":" + createModel.currency + ",\"intent\":" + createModel.intent + ",\"merchantInvoiceNumber\":" + createModel.invoiceNumber + "}", ParameterType.RequestBody);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                var r = response.ResponseStatus;
                if (response != null && response.Content.Contains("The incoming token has expired"))
                {
                    bkash = await GrantBkashPGWToken();
                    return await CreatePayment(createModel);
                }
                returnModel = JsonSerializer.Deserialize<BkashCreatePaymentReturnModel>(response.Content);
            }
            catch (Exception e)
            {
                var res = e.Message;
                if (res == "Request timed out")
                {
                    returnModel.errorMessage = "Timed Out.\nStart Payment again";
                }
                else  
                    returnModel.errorMessage = "Exception Occured";
            }
            return returnModel;
        }

        public async Task<BkashExecutePaymentReturnModel> ExecutePayment(BkashExecutePaymentInitModel createModel)
        {
            BkashExecutePaymentReturnModel returnModel = new BkashExecutePaymentReturnModel();
            if (createModel == null)
                return returnModel;
            BkashCredentials bkash = await GetBkashToken();
            var options = new RestClientOptions(_configuration.GetSection("Bkash")["ExecutePaymentUrl"] + "/" + createModel.paymentID)
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000  // 30second
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bkash.Token);
            request.AddHeader("X-APP-Key", bkash.AppKey);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response != null && response.Content.Contains("The incoming token has expired"))
                {
                    bkash = await GrantBkashPGWToken();
                    return await ExecutePayment(createModel);
                }
                returnModel = JsonSerializer.Deserialize<BkashExecutePaymentReturnModel>(response.Content);
            }
            catch (Exception e)
            {
                var message = e.Message;
                if(message== "Request timed out")
                {
                    BkashQueryPaymentReturnModel queryPaymentReturnModel = await QueryPayment(createModel.paymentID);
                    if(queryPaymentReturnModel.transactionStatus== "Completed")
                    {
                        BkashExecutePaymentReturnModel result = _mapper.Map<BkashQueryPaymentReturnModel, BkashExecutePaymentReturnModel>(queryPaymentReturnModel);
                        return result;
                    }
                    else
                    {
                        returnModel.errorMessage = "Timed Out.\nStart Payment again";
                    }
                }
                else
                returnModel.errorMessage = "Exception Occured";
            }
            return returnModel;
        }



        public async Task<BkashQueryPaymentReturnModel> QueryPayment(string paymentID)
        {
            BkashQueryPaymentReturnModel returnModel = new BkashQueryPaymentReturnModel();
            if (paymentID==null)
            {
                return returnModel;
            }
            BkashCredentials bkash = await GetBkashToken();
            var options = new RestClientOptions(_configuration.GetSection("Bkash")["QueryPaymentUrl"] + "/" + paymentID)
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000  // 30second
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bkash.Token);
            request.AddHeader("X-APP-Key", bkash.AppKey);
            returnModel.errorMessage = "Invalid Payment ID";
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response != null && response.Content.Contains("The incoming token has expired"))
                {
                    bkash = await GrantBkashPGWToken();
                    return await QueryPayment(paymentID);
                }
                returnModel = JsonSerializer.Deserialize<BkashQueryPaymentReturnModel>(response.Content);
            }
            catch (Exception e)
            {
                returnModel.errorMessage = "Exception Occured";
            }
            return returnModel;
        }

        public async Task<BkashSearchTransactionReturnModel> SearchTransaction(string trxID)
        {
            BkashSearchTransactionReturnModel returnModel = new BkashSearchTransactionReturnModel();
            if(trxID == null)
            {
                return returnModel;
            }
            BkashCredentials bkash = await GetBkashToken();
            var options = new RestClientOptions(_configuration.GetSection("Bkash")["SearchTransactionUrl"] + "/" + trxID)
            {
                ThrowOnAnyError = true,
                MaxTimeout = 30000  // 30second
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bkash.Token);
            request.AddHeader("X-APP-Key", bkash.AppKey);
            try
            {
                RestResponse response = await client.ExecuteAsync(request);
                if (response != null && response.Content.Contains("The incoming token has expired"))
                {
                    bkash = await GrantBkashPGWToken();
                    return await SearchTransaction(trxID);
                }
                returnModel = JsonSerializer.Deserialize<BkashSearchTransactionReturnModel>(response.Content);
            }
            catch (Exception e)
            {
                returnModel.errorMessage = "Exception Occured";
            }
            return returnModel;
        }



        public async Task<int> ModifyInvoiceStatus(string InvoiceNo, int Status)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<int>(new
                {
                    InvoiceNo = InvoiceNo,
                    StatusType = Status
                }, ModifyInvoiceStatusSP);
                return result;
            }
            catch (Exception ex)
            {
                return 5;
            }
        }

        public async Task<string> GenerateInvoiceNumber()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    
                }, GenerateInvoiceNumberSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<dynamic> InsertTransactionDB(BkashSearchTransactionReturnModel searchTransaction)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<dynamic>(new
                {
                    TRXID = searchTransaction.trxID,
                    CUSTOMERMSISDN = searchTransaction.customerMsisdn,
                    AMOUNT = searchTransaction.amount,
                    COMPLETEDTIME = searchTransaction.completedTime,
                    TRANSACTIONREFERENCE = searchTransaction.transactionReference,
                    TRANSACTIONSTATUS = searchTransaction.transactionStatus,
                    INITITATIONTIME = searchTransaction.initiationTime,
                    CURRENCY = searchTransaction.currency,
                    ORGANIZATIONSHORTCODE = searchTransaction.organizationShortCode,
                    TRANSACTIONTYPE = searchTransaction.transactionType,
                    ERRORCODE = searchTransaction.errorCode,
                    ERRORMESSAGE = searchTransaction.errorMessage,
                    INSERTTIME = DateTime.UtcNow.AddHours(6)
                }, InsertTransactionDBSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<dynamic> InsertPaymentDB(BkashExecutePaymentReturnModel model, string Payer,string Event,string Name,string Email,string Phone,string Address)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<dynamic>(new
                {
                    PAYER = Payer,
                    CUSTOMERMSISDN = model.customerMsisdn,
                    MERCHANTINVOICENUMBER = model.merchantInvoiceNumber,
                    PAYMENTID = model.paymentID,
                    CREATETIME = model.createTime,
                    UPDATETIME = model.updateTime,
                    TRXID = model.trxID,
                    TRANSACTIONSTATUS = model.transactionStatus,
                    AMOUNT = model.amount,
                    CURRENCY = model.currency,
                    INTENT = model.intent,
                    INSERTTIME = DateTime.UtcNow.AddHours(6),
                    METHOD = "bKash",
                    EVENT = Event,
                    NAME = Name,
                    EMAIL = Email,
                    PHONE = Phone,
                    ADDRESS = Address,
                }, InsertPaymentDBSP);
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
