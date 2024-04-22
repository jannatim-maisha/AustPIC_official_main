using AustPICWeb.Repositories.Contest;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Repositories.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    [Authorize]
    public class ContestController : Controller
    {
        private readonly IContestRepository _contestRepository;
        private readonly ICssRepository _cssRepository;

        public ContestController(IContestRepository contestRepository, ICssRepository cssRepository)
        {
            _contestRepository = contestRepository;
            _cssRepository = cssRepository;
        }

        public async Task<IActionResult> Index()
        {
            var contests = await _contestRepository.GetContestList();
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(contests);
        }

        public async Task<IActionResult> Details(int id)
        {
            var detail = await _contestRepository.GetContestDetail(id);
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(detail);
        }
    }
}
