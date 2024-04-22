using AustPICWeb.Repositories;
using AustPICWeb.Repositories.Committee;
using AustPICWeb.Repositories.CssVariable;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    public class CommitteeController : Controller
    {
        private readonly ICommitteeRepository _committeeRepository;
        private readonly ICssRepository _cssRepository;

        public CommitteeController(ICommitteeRepository committeeRepository, ICssRepository cssRepository)
        {
            _committeeRepository = committeeRepository;
            _cssRepository = cssRepository;
        }

        //public async Task<IActionResult> Index()
        //{
        //    //var committee = repository.GetCommitteeData();
        //    var committee = await _testService.GetMemberList();

        //    return View(committee);
        //}

        public async Task<IActionResult> Index(String id)
        {
            ViewBag.Semester = id;
            var committee = await _committeeRepository.GetMemberListBySemester(id);
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;

            return View(committee);
        }
    }
}
