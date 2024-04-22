using AustPIC.db;
using AustPIC.Models;

namespace AustPICWeb.Repositories.Blog
{
    public interface IBlogRepository
    {
        Task<List<BlogModel>> GetBlogList();
        Task<List<BlogModel>> GetBlogListByCategory(string category);
        Task<List<BlogModel>> GetBlogListByDate(string date);
        Task<List<BlogModel>> GetTop2BlogList();
        Task<BlogModel> GetBlogDetail(int id);
        //Task<BlogModel> GetTopBlog();
        Task<List<BlogModel>> GetTopBlog();
        Task<List<String>> GetBlogCategoryList();
        Task<List<String>> GetBlogDateList();
        Task<dynamic> AddBlogDetail(BlogModel blog);
    }
}
