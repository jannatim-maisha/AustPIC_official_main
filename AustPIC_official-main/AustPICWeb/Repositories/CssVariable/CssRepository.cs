using AustPIC.Models;
using AustPICWeb.DBContexts;

namespace AustPICWeb.Repositories.CssVariable
{
    public class CssRepository : ICssRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string GetAllVariableSP = "AustPIC_GetAllCssVariables";

        public CssRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<List<CssVariableModel>> GetCssVariablesList()
        {
            //Console.WriteLine("CSS DB accessed");
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<CssVariableModel>(new
                {
                }, GetAllVariableSP);
                List<CssVariableModel> list = (List<CssVariableModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<CssVariableModel> { new CssVariableModel() };
            }
        }
    }
}
