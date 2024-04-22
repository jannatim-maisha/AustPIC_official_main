using System.Collections.Generic;
using System.Linq;
using AustPIC.Models;

namespace AustPIC.db.DbOperations
{
    public class EventRepository
    {
        public int AddEvent(EventModel model)
        {
            using (var context = new AustPICEntities())
            {
                Event evnt = new Event()
                {
                    EventTitle = model.EventTitle,
                    EventDesc = model.EventDesc,
                    EventImg = model.EventImg,
                    EventStart = model.EventStart,
                    EventEnd = model.EventEnd
                };

                context.Event.Add(evnt);
                context.SaveChanges();

                return evnt.EventId;
            }
        }

        public List<EventModel> GetAllEvent()
        {
            using (var context = new AustPICEntities())
            {
                var result = context.Event.Select(x => new EventModel()
                {
                    EventId = x.EventId,
                    EventTitle = x.EventTitle,
                    EventDesc = x.EventDesc,
                    EventImg = x.EventImg,
                    EventStart = x.EventStart,
                    EventEnd = x.EventEnd
                }).ToList();

                return result;
            }
        }

        public EventModel GetEvent(int id)
        {
            using (var context = new AustPICEntities())
            {
                var result = context.Event
                    .Where(x => x.EventId == id)
                    .Select(x => new EventModel()
                    {
                        EventId = x.EventId,
                        EventTitle = x.EventTitle,
                        EventDesc = x.EventDesc,
                        EventImg = x.EventImg,
                        EventStart = x.EventStart,
                        EventEnd = x.EventEnd
                    }).FirstOrDefault();

                return result;
            }
        }

        public bool UpdateEvent(int id, EventModel model)
        {
            using (var context = new AustPICEntities())
            {
                var evnt = new Event();

                if (evnt != null)
                {
                    evnt.EventId = id;
                    evnt.EventTitle = model.EventTitle;
                    evnt.EventDesc = model.EventDesc;
                    evnt.EventImg = model.EventImg;
                    evnt.EventStart = model.EventStart;
                    evnt.EventEnd = model.EventEnd;
                }

                context.Entry(evnt).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return true;

            }
        }

        public bool DeleteEvent(int id)
        {
            using (var context = new AustPICEntities())
            {
                var evnt = new Event()
                {
                    EventId = id
                };

                context.Entry(evnt).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();

                return false;
            }
        }


    }


}
