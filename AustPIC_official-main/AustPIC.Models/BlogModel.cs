using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AustPIC.Models
{
    public class BlogModel
    {
        public int BlogId { get; set; }
        [Required(ErrorMessage ="Please give the blog a suitable name")]
        [DisplayName("Blog Title")]
        public string BlogTitle { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime BlogDate { get; set; }
        [Required(ErrorMessage = "Please enter the name of the blog author")]
        [DisplayName("Blog Author")]
        public string BlogAuthor { get; set; }
        [Required(ErrorMessage = "Please specify blog category")]
        [DisplayName("Blog Category")]
        public string BlogCatergory { get; set; }
        [Required(ErrorMessage = "Please enter a short description of the blog")]
        [DisplayName("Blog Description")]
        public string BlogShort { get; set; }
        [Required]
        [DisplayName("Blog Content")]
        public string BlogBody { get; set; }
        public string BlogImg { get; set; }
        public int BlogView { get; set; }
        [Required(ErrorMessage = "Please upload an image for the blog")]
        [DisplayName("Upload Blog Image")]
        public IFormFile BlogImgFile { get; set; }
        public string BlogClass { get; set; }
    }
}
