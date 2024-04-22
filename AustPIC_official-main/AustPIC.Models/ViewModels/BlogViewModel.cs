using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustPIC.Models.ViewModels
{
    public class BlogViewModel
    {
        public IEnumerable<BlogModel> blogList { get; set; }
        public IEnumerable<BlogModel> blogTop2 { get; set; }
        public List<String> category { get; set; }
        public List<String> date { get; set; }
    }
}
