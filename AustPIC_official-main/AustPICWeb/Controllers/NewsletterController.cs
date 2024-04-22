using AustPIC.db;
using AustPIC.Models;
using AustPICWeb.Repositories.Committee;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Repositories.Newsletter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AustPICWeb.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterRepository _newsletterRepository;
        public NewsletterController(INewsletterRepository newsletterRepository)
        {
            _newsletterRepository = newsletterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewsletterSubscriber newsletter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _newsletterRepository.SaveEmail(newsletter);
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                    {
                        // Unique constraint violation - email already exists in the database
                        return StatusCode(409, "duplicate");
                    }

                    // Some other database error occurred
                    return StatusCode(500, "error");
                }
            }

            return View();
        }

    }
}
