using Microsoft.AspNetCore.Mvc;
using AustPIC.db.DbOperations;
using AustPIC.Models;
using Microsoft.AspNetCore.Authorization;
using AustPIC.Models.ViewModels;
using System.Net.Mail;
using System.Net;

namespace AustPICWeb.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        CommitteeRepository CmtRepository = null;
        EventRepository EvntRepository = null;
        //ContactRepository contactRepository = null;
        CssRepository CssRepository = null;
        private IConfiguration Configuration;

        public AdminController(IConfiguration _configuration)
        {
            CmtRepository = new CommitteeRepository();
            EvntRepository = new EventRepository();
            //contactRepository = new ContactRepository();
            CssRepository = new CssRepository();
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCommittee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCommittee(CommitteeModel model)
        {
            if (ModelState.IsValid)
            {
                int id = CmtRepository.AddCommittee(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.success = "Data Added";
                }
            }
            return View();
        }

        public IActionResult GetAllCommittee()
        {
            var result = CmtRepository.GetAllCommittee();
            return View(result);
        }

        public IActionResult CommitteeDetails(int id)
        {
            var committee = CmtRepository.GetCommittee(id);
            return View(committee);
        }

        [HttpGet]
        public IActionResult EditCommittee(int id)
        {
            var committee = CmtRepository.GetCommittee(id);
            return View(committee);
        }

        [HttpPost]
        public IActionResult EditCommittee(CommitteeModel model)
        {
            if (ModelState.IsValid)
            {
                CmtRepository.UpdateCommittee(model.CommitteeId, model);
                return RedirectToAction("GetAllCommittee");
            }
            return View();
        }

        public IActionResult DeleteCommittee(int id)
        {
            CmtRepository.DeleteCommittee(id);
            return RedirectToAction("GetAllCommittee");
        }

        ////  Event ////

        [HttpGet]
        public IActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                int id = EvntRepository.AddEvent(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.success = "Data Added";
                }
            }
            return View();
        }

        public IActionResult GetAllCss()
        {
            var result = CssRepository.GetAllCssVariables();
            return View(result);
        }

        [HttpGet]
        public IActionResult EditCss(int id)
        {
            var evnt = CssRepository.GetCssVariables(id);
            return View(evnt);
        }

        [HttpPost]
        public IActionResult EditCss(CssVariableModel model)
        {
            if (ModelState.IsValid)
            {
                CssRepository.UpdateCssVariables(model.VarId, model);
                return RedirectToAction("GetAllCss");
            }
            return View();
        }

        public IActionResult GetAllEvent()
        {
            var result = EvntRepository.GetAllEvent();
            return View(result);
        }

        public IActionResult EventDetails(int id)
        {
            var evnt = EvntRepository.GetEvent(id);
            return View(evnt);
        }

        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            var evnt = EvntRepository.GetEvent(id);
            return View(evnt);
        }

        [HttpPost]
        public IActionResult EditEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                EvntRepository.UpdateEvent(model.EventId, model);
                return RedirectToAction("GetAllEvent");
            }
            return View();
        }

        public IActionResult DeleteEvent(int id)
        {
            EvntRepository.DeleteEvent(id);
            return RedirectToAction("GetAllEvent");
        }

        //public IActionResult GetAllContact()
        //{
        //    var result = contactRepository.GetAllContact();
        //    if (result != null)
        //    {
        //        return View(result);
        //    }
        //    return View();
        //}

        //public IActionResult ContactDetails(int id)
        //{
        //    var result = contactRepository.GetContact(id);
        //    return View(result);
        //}

        //public IActionResult ReplyMessage(string email)
        //{           
        //    ViewBag.Email = email;
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ReplyMessage(EmailReplyModel model, int id)
        //{
        //    //Read SMTP settings from AppSettings.json.
        //    string host = this.Configuration.GetValue<string>("Smtp:Server");
        //    int port = this.Configuration.GetValue<int>("Smtp:Port");
        //    string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
        //    string userName = this.Configuration.GetValue<string>("Smtp:UserName");
        //    string password = this.Configuration.GetValue<string>("Smtp:Password");

        //    using (MailMessage mm = new MailMessage(fromAddress, model.Email))
        //    {
        //        mm.Subject = model.Subject;
        //        mm.Body = "<br /><br />Email: " + model.Email + "<br />" + model.Body;
        //        mm.IsBodyHtml = true;

        //        using (SmtpClient smtp = new SmtpClient())
        //        {
        //            smtp.Host = host;
        //            smtp.EnableSsl = true;
        //            NetworkCredential NetworkCred = new NetworkCredential(userName, password);
        //            //smtp.UseDefaultCredentials = true;
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = NetworkCred;
        //            smtp.Port = port;
        //            smtp.Send(mm);                   
        //            contactRepository.UpdateReplyStatus(id);
        //            ViewBag.Message = "Email sent sucessfully.";
        //        }
        //    }

        //    return View();
        //}
    }
}
