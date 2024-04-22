using AustPIC.db.DbOperations;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Repositories.Event;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    public class WorkshopController : Controller
    {
        private readonly IEventRepository _testService;
        private readonly ICssRepository _cssRepository;

        public WorkshopController(IEventRepository testService, ICssRepository cssRepository)
        {
            _testService = testService;
            _cssRepository = cssRepository;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _testService.GetEventList();
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var detail = await _testService.GetEventDetail(id);
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(detail);
        }
    }
}
