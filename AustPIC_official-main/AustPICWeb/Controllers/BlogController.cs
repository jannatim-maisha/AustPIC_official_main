using AustPIC.Models;
using AustPIC.Models.ViewModels;
using AustPICWeb.Repositories.Blog;
using AustPICWeb.Repositories.CssVariable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICssRepository _cssRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(IBlogRepository blogRepository, ICssRepository cssRepository, IWebHostEnvironment webHostEnvironment)
        {
            _blogRepository = blogRepository;
            _cssRepository = cssRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string category, string date)
        {
            //var blogTop2 = await _blogRepository.GetTop2BlogList();
            //var topblog = await _blogRepository.GetTopBlog();
            var categoryList = await _blogRepository.GetBlogCategoryList();
            var dateList = await _blogRepository.GetBlogDateList();
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;

            if (category != null)
            {
                ViewBag.Category = category;
                var blogList = await _blogRepository.GetBlogListByCategory(category);
                var model = new BlogViewModel { blogList = blogList, category = categoryList, date = dateList };

                return View(model);
            }
            else if (date != null)
            {
                ViewBag.Date = date;
                var blogList = await _blogRepository.GetBlogListByDate(date);
                var model = new BlogViewModel { blogList = blogList, category = categoryList, date = dateList };

                return View(model);
            }
            else
            {
                var blogList = await _blogRepository.GetBlogList();
                var model = new BlogViewModel { blogList = blogList, category = categoryList, date = dateList };

                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoryList = await _blogRepository.GetBlogCategoryList();
            var dateList = await _blogRepository.GetBlogDateList();
            ViewBag.Category = categoryList;
            ViewBag.Date = dateList;
            var detail = await _blogRepository.GetBlogDetail(id);

            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(detail);
        }

        public async Task<IActionResult> CreateBlog()
        {
            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateBlog(BlogModel blog, IFormFile img)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (img != null && img.Length > 0)
        //        {
        //            string folder = "blogs/image/";
        //            folder += Guid.NewGuid().ToString() + "_" + img.FileName;

        //            blog.BlogImg = folder;

        //            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

        //            await img.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        //        }

        //        await _blogRepository.AddBlogDetail(blog);
        //        return Ok();
        //    }

        //    var cssVariables = await _cssRepository.GetCssVariablesList();
        //    ViewBag.CssVariables = cssVariables;
        //    return View(blog);
        //} 

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.BlogImgFile != null && blog.BlogImgFile.Length > 0)
                {
                    string folder = "blogs/image/";
                    folder += Guid.NewGuid().ToString() + "_" + blog.BlogImgFile.FileName;

                    blog.BlogImg = folder;

                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await blog.BlogImgFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                try
                {
                    if(blog.BlogClass != "negative")
                    {
                        await _blogRepository.AddBlogDetail(blog);
                    }
                    return Ok();
                }
                catch
                (Exception ex)
                {
                    return StatusCode(500, "error");
                }

            }

            var cssVariables = await _cssRepository.GetCssVariablesList();
            ViewBag.CssVariables = cssVariables;
            return View(blog);
        }


    }
}
