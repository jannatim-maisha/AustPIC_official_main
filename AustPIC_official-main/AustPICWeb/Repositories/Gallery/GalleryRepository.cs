using AustPIC.db.DbOperations;
using AustPIC.Models;
using AustPICWeb.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustPICWeb.Repositories.Gallery
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string GetAllGallerySP = "AustPIC_GetAllGallery";

        public GalleryRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }           
        public async Task<List<GalleryModel>> GetGalleryList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<GalleryModel>(new
                {
                }, GetAllGallerySP);
                List<GalleryModel> list = (List<GalleryModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<GalleryModel> { new GalleryModel() };
            }
        }

    }
}

