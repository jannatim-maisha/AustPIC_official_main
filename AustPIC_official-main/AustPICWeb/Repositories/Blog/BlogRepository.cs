using AustPIC.Models;
using AustPICWeb.DBContexts;
using System.Net.Mail;

namespace AustPICWeb.Repositories.Blog
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string GetAllBlogSP = "AustPIC_GetAllBlog";
        string GetTop2BlogSP = "AustPIC_GetTopBlog";
        string GetBlogSP = "AustPIC_GetBlog";
        string GetTopBlogSP = "AustPIC_GetTop1Blog";
        string GetBlogCategorySP = "AustPIC_GetAllBlogCategory";
        string GetBlogByCategorySP = "AustPIC_GetAllBlogByCategory";
        string GetBlogDateSP = "AustPIC_GetAllBlogDate";
        string GetBlogByDateSP = "AustPIC_GetAllBlogByDate";
        string InsertBlogSP = "AustPIC_InsertBlog";

        public BlogRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<List<BlogModel>> GetBlogList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<BlogModel>(new
                {
                }, GetAllBlogSP);
                List<BlogModel> list = (List<BlogModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<BlogModel> { new BlogModel() };
            }
        }

        public async Task<List<BlogModel>> GetBlogListByCategory(string category)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<BlogModel>(new
                {
                    category = category
                }, GetBlogByCategorySP);
                List<BlogModel> list = (List<BlogModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<BlogModel> { new BlogModel() };
            }
        }

        public async Task<List<BlogModel>> GetBlogListByDate(string date)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<BlogModel>(new
                {
                    date = date
                }, GetBlogByDateSP);
                List<BlogModel> list = (List<BlogModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<BlogModel> { new BlogModel() };
            }
        }

        public async Task<List<BlogModel>> GetTop2BlogList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<BlogModel>(new
                {
                }, GetTop2BlogSP);
                List<BlogModel> list = (List<BlogModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<BlogModel> { new BlogModel() };
            }
        }

        public async Task<BlogModel> GetBlogDetail(int id)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<BlogModel>(new
                {
                    id = id
                }, GetBlogSP);
                return result;
            }
            catch (Exception ex)
            {
                return new BlogModel();
            }
        }

        public async Task<List<BlogModel>> GetTopBlog()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<BlogModel>(new
                {
                }, GetTopBlogSP);
                List<BlogModel> list = (List<BlogModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<BlogModel> { new BlogModel() };
            }
        }

        public async Task<List<String>> GetBlogCategoryList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<String>(new
                {
                }, GetBlogCategorySP);
                List<String> list = (List<String>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<String>();
            }
        }

        public async Task<List<String>> GetBlogDateList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<String>(new
                {
                }, GetBlogDateSP);
                List<String> list = (List<String>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<String>();
            }
        }

        public async Task<dynamic> AddBlogDetail(BlogModel blog)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<dynamic>(new
                {
                    BlogTitle = blog.BlogTitle,
                    BlogDate = DateTime.UtcNow.AddHours(6),
                    BlogAuthor = blog.BlogAuthor,
                    BlogCatergory = blog.BlogCatergory,
                    BlogShort = blog.BlogShort,
                    BlogBody = blog.BlogBody,
                    BlogImg = blog.BlogImg,
                    BlogView = blog.BlogView, 
                }, InsertBlogSP);
                
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
