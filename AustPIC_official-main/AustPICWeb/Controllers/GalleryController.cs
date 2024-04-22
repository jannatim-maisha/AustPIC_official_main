using AustPIC.db.DbOperations;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Repositories.Gallery;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ICssRepository _cssRepository;

        public GalleryController(IGalleryRepository galleryRepository, ICssRepository cssRepository)
        {
            _galleryRepository = galleryRepository;
            _cssRepository = cssRepository;      
        }

        public async Task<IActionResult> Index()
        {
            var gallery = await _galleryRepository.GetGalleryList();
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;

            return View(gallery);
        }
    }
}
