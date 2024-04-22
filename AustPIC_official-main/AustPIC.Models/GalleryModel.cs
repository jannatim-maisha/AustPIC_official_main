using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AustPIC.Models
{
    public class GalleryModel
    {
        public int ImgId { get; set; }
        public string ImgTitle { get; set; }
        public string ImgDesc { get; set; }
        public string ImgUrl { get; set; }
    }

}
