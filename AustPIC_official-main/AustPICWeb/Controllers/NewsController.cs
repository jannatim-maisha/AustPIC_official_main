using AustPICWeb.Repositories.Contest;
using AustPICWeb.Repositories.CssVariable;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    public class NewsController : Controller
    {
        private readonly IContestRepository _contestRepository;
        private readonly ICssRepository _cssRepository;

        public NewsController(IContestRepository contestRepository, ICssRepository cssRepository)
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

    }
}
