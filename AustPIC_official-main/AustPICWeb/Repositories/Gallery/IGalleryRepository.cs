using AustPIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustPICWeb.Repositories.Gallery
{
    public interface IGalleryRepository
    {
        Task<List<GalleryModel>> GetGalleryList();
    }
}
