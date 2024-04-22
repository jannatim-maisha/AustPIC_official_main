using AustPIC.Models;

namespace AustPICWeb.Repositories.Event
{
    public interface IEventRepository
    {
        Task<List<EventModel>> GetEventList();
        Task<EventModel> GetEventDetail(int id);
    }
}
