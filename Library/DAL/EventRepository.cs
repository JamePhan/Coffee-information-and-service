using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library.DAL
{
    public class EventRepository : IEventRepository
    {
        private CoffeehouseSystemContext _context;
        private IMapper _mapper;

        public EventRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<EventInfo> GetEvents(int count)
        {
            List<Event> events;

            if (count > 0)
            {
                events = _context.Events.Take(count).ToList();
            }
            else
            {
                events = _context.Events.ToList();
            }

            return _mapper.Map<List<Event>, List<EventInfo>>(events);
        }

        public List<EventInfo> GetLastest(int count)
        {
            List<Event> events;
            if (count > 0)
            {
                events = _context.Events.OrderByDescending(events => events.Date).Take(count).ToList();
            }
            else
            {
                events = _context.Events.OrderByDescending(events => events.Date).ToList();
            }
            return _mapper.Map<List<Event>, List<EventInfo>>(events);
        }
    }
}