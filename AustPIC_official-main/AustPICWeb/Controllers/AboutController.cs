using AustPICWeb.Repositories.CssVariable;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    public class AboutController : Controller
    {
        private readonly ICssRepository _cssRepository;

        public AboutController(ICssRepository cssRepository)
        {
            _cssRepository = cssRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View();
        }
    }
}
