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

        public List<EventInfo> GetEvents()
        {
            List<Event> events = _context.Events.ToList();
            return _mapper.Map<List<Event>, List<EventInfo>>(events);
        }
    }
}