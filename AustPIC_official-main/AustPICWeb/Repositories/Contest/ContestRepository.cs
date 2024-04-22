using AustPIC.Models;
using AustPICWeb.DBContexts;

namespace AustPICWeb.Repositories.Contest
{
    public class ContestRepository : IContestRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string GetAllContestSP = "AustPIC_GetAllContest";
        string GetContestSP = "AustPIC_GetContest";

        public ContestRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<List<ContestModel>> GetContestList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<ContestModel>(new
                {
                }, GetAllContestSP);
                List<ContestModel> list = (List<ContestModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<ContestModel> { new ContestModel() };
            }
        }

        public async Task<ContestModel> GetContestDetail(int id)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<ContestModel>(new
                {
                    id = id
                }, GetContestSP);
                return result;
            }
            catch (Exception ex)
            {
                return new ContestModel();
            }
        }
    }
}
