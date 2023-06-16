using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library.DAL
{
    public class EventRepository : IEventRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}