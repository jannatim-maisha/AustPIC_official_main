using ClubWebsite.DBContexts;
using ClubWebsite.Models.Common;

namespace ClubWebsite.Service.Template
{
    public class TemplateService : ITemplateService
    {
        #region Fields

        private readonly IDapperDBContext _dapperDBContext;
        private readonly IConfiguration _configuration;
        String GetTemplateBodySP = "ClubWebsite_GetActualDataFromTemplateBody";
        #endregion

        #region Ctor

        public TemplateService(IDapperDBContext dapperDBContext, IConfiguration configuration)
        {
            _dapperDBContext = dapperDBContext;
            _configuration = configuration;
        }

        #endregion


        #region Methods
        public async Task<string> GetTemplateBody(int TemplateID,string ClubID, string InvoiceNo)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    TEMPLATEID = TemplateID,
                    CLUBID = ClubID,
                    INVOICENO = InvoiceNo
                }, GetTemplateBodySP);
                
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
}
