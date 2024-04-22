using AustPIC.db.DbOperations;
using AustPIC.Models;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace AustPICWeb.Controllers
{
    public class ContactController : Controller
    {
        //ContactRepository ContactRepository = null;
        private readonly ICssRepository _cssRepository;
        private readonly IEmailSender _emailSender;

        //public ContactController()
        //{
        //    ContactRepository = new ContactRepository();
        //}

        public ContactController(ICssRepository cssRepository, IEmailSender emailSender)
        {
            _cssRepository = cssRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactModel contactModel)
        {
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            await _emailSender.SendEmailAsync(contactModel.ContactEmail, contactModel.ContactSubject, contactModel.ContactMessage);
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(ContactModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int id = ContactRepository.AddContact(model);
        //        if (id > 0)
        //        {
        //            ModelState.Clear();
        //            ViewBag.success = "Data Added";
        //        }
        //    }
        //    var cssVariables = await _cssRepository.GetCssVariablesList();
        //    ViewBag.CssVariables = cssVariables;
        //    return View();
        //}
    }
}
