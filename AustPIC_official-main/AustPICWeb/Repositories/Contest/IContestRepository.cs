using AustPIC.Models;

namespace AustPICWeb.Repositories.Contest
{
    public interface IContestRepository
    {
        Task<List<ContestModel>> GetContestList();
        Task<ContestModel> GetContestDetail(int id);
    }
}
