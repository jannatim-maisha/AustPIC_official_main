using AustPIC.db.DbOperations;
using AustPIC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AustPIC.Models.ViewModels;
using AustPICWeb.Repositories.Committee;
using AustPICWeb.Repositories.CssVariable;

namespace AustPICWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommitteeRepository _committeeRepository;
        private readonly ICssRepository _cssRepository;
        public HomeController(ICommitteeRepository committeeRepository, ICssRepository cssRepository)
        {
            _committeeRepository = committeeRepository;
            _cssRepository = cssRepository;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            //var committee = repository.GetCommitteeData();
            var committee = await _committeeRepository.GetTopMemberList();
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;

            return View(committee);
        }

        //public IActionResult Index()
        //{
        //    var committee = repository.GetCommitteeData();
        //    return View(committee);
        //}     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}