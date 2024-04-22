using AustPIC.db.DbOperations;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Repositories.Event;
using AustPICWeb.Repositories.Gallery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    [Authorize] 
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICssRepository _cssRepository;

        public EventController(IEventRepository eventRepository, ICssRepository cssRepository)
        {
            _eventRepository = eventRepository;
            _cssRepository = cssRepository;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.GetEventList();
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var detail = await _eventRepository.GetEventDetail(id);
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(detail);
        }
    }
}
