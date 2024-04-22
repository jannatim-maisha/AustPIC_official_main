using AutoMapper;
using ClubWebsite.Service.Payment;
using Microsoft.AspNetCore.Mvc;
using ClubWebsite.Models.Payment;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Azure;
using ClubWebsite.Models.Common;
using ClubWebsite.Service.Join;
using AutoMapper.Execution;
using ClubWebsite.Models.Join;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ClubWebsite.Models;
using System.Diagnostics;

namespace ClubWebsite.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IJoinService _joinService;

        public PaymentController(IPaymentService paymentService, IMapper mapper, IConfiguration configuration,IJoinService joinService)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _configuration = configuration;
            _joinService = joinService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Fee = _configuration.GetSection("Payment")["GetGMRecruitmentFee"];
                var cookie = HttpContext.User.FindFirstValue("Join-AUSTPIC-Member-Token");
                List<MemberToken> tokens = await _joinService.GetTokenForMember(cookie);
                if (tokens.Count > 0)
                {
                    ViewBag.Name = tokens[0].Name;
                    ViewBag.Email = tokens[0].Email;
                    ViewBag.MobileNo = tokens[0].MobileNo;
                }
                else
                {
                    ViewBag.Name = "Not Found";
                    ViewBag.Email = "Not Found";
                    ViewBag.MobileNo = "Not Found";
                }
            }
            catch(Exception e)
            {
                ViewBag.Name = "Not Found";
                ViewBag.Email = "Not Found";
                ViewBag.MobileNo = "Not Found";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<BkashCreatePaymentReturnModel> CreatePaymentInit()
        {
            try
            {
                var cookie = HttpContext.User.FindFirstValue("Join-AUSTPIC-Member-Token");
                if (cookie == null || !await _joinService.VerifyTokenForMember(cookie))
                {
                    BkashCreatePaymentReturnModel model = new BkashCreatePaymentReturnModel();
                    model.errorMessage = "UnAuthorized";
                    return model;
                }
                BkashCreatePaymentInitModel createModel = new BkashCreatePaymentInitModel();
                createModel.amount = _configuration.GetSection("Payment")["GetGMRecruitmentFee"];
                createModel.intent = "sale";
                createModel.invoiceNumber = await _paymentService.GenerateInvoiceNumber();
                createModel.currency = "BDT";
                return await _paymentService.CreatePayment(createModel);
            }
            catch(Exception e)
            {
                return new BkashCreatePaymentReturnModel();
            }
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<BkashExecutePaymentReturnModel> ExecutePaymentInit(string PaymentID)
        {
            try
            {
                var cookie = HttpContext.User.FindFirstValue("Join-AUSTPIC-Member-Token");
                if (cookie == null || !await _joinService.VerifyTokenForMember(cookie))
                {
                    BkashExecutePaymentReturnModel model = new BkashExecutePaymentReturnModel();
                    model.errorMessage = "UnAuthorized";
                    return model;
                }
                BkashExecutePaymentInitModel executionCreateModel = new BkashExecutePaymentInitModel();
                executionCreateModel.paymentID = PaymentID;

                BkashExecutePaymentReturnModel executionReturnModel = await _paymentService.ExecutePayment(executionCreateModel);
                if (executionReturnModel != null && executionReturnModel.paymentID != null)
                {
                    try
                    {
                        //BkashSearchTransactionReturnModel returnModel = await _paymentService.SearchTransaction(executionReturnModel.trxID);
                        //await _paymentService.InsertTransactionDB(returnModel);
                        await _joinService.InsertNewMemberFromTemp(executionReturnModel, cookie);
                    }
                    catch (Exception e)
                    {

                    }
                    await HttpContext.SignOutAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme);
                }
                return executionReturnModel;
            }
            catch(Exception e)
            {
                return new BkashExecutePaymentReturnModel();
            }
        }
        [Authorize]
        private async Task<BkashQueryPaymentReturnModel> QueryPaymentInit(string paymentID)
        {
            return await _paymentService.QueryPayment(paymentID);
        }
        [Authorize]
        private async Task<BkashSearchTransactionReturnModel> SearchTransactionInit(string trxId)
        {
            return await _paymentService.SearchTransaction(trxId);
        }
    }
}
