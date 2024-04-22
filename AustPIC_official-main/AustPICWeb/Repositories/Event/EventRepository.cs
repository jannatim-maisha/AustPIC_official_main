using AustPIC.Models;
using AustPICWeb.DBContexts;

namespace AustPICWeb.Repositories.Event
{
    public class EventRepository : IEventRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string GetAllEventSP = "AustPIC_GetAllEvent";
        string GetEventSP = "AustPIC_GetEvent";

        public EventRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<List<EventModel>> GetEventList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<EventModel>(new
                {
                }, GetAllEventSP);
                List<EventModel> list = (List<EventModel>)result;
                return list;
            }
            catch (Exception ex)
            {
                return new List<EventModel> { new EventModel() };
            }
        }

        public async Task<EventModel> GetEventDetail(int id)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<EventModel>(new
                {
                    id = id
                }, GetEventSP);
                return result;
            }
            catch (Exception ex)
            {
                return new EventModel();
            }
        }
    }
}
