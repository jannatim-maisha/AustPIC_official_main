using AutoMapper;
using ClubWebsite.Models.Payment;
using ClubWebsite.Models.Sms;
using ClubWebsite.Service.Payment;
using ClubWebsite.Service.Sms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
namespace ClubWebsite.Controllers
{
    [Authorize]
    public class SmsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISmsService _smsService;

        public SmsController(IConfiguration configuration,ISmsService smsService)
        {
            _configuration = configuration;
            _smsService = smsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        private async Task<SendSMSResultModel> SendSMSInit(string message, string number, bool IsBangla)
        {
            SendSMSInitModel createSms = new SendSMSInitModel();
            createSms.Is_Flash = false;
            createSms.Is_Unicode = IsBangla;
            createSms.MobileNumbers = number;
            createSms.Message = message;
            createSms.SecretKey = _configuration.GetSection("SMSQ")["SecretKey"];
            SendSMSResultModel ans = await _smsService.SendSMS(createSms);
            return ans;
        }
    }
}
