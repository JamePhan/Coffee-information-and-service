using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public List<EventInfo> GetEvents()
        {
            List<Event> events;

            events = _context.Events
                .Include(locale => locale.Location)
                .Include(group => group.GroupImage)
                .ThenInclude(image => image.Image).ToList();

            return _mapper.Map<List<Event>, List<EventInfo>>(events);
        }

        public List<EventInfo> GetLastest()
        {
            List<Event> events;

            events = _context.Events
                .Include(locale => locale.LocationId)
                .OrderByDescending(events => events.Date)
                .Include(group => group.GroupImage)
                .ThenInclude(image => image.Image).ToList();

            return _mapper.Map<List<Event>, List<EventInfo>>(events);
        }

        public EventInfo? GetEvent(int id)
        {
            Event? eve = _context.Events.Include(group => group.GroupImage).ThenInclude(image => image.Image).FirstOrDefault(ev => ev.EventId.Equals(id));
            if (eve != null)
            {
                return _mapper.Map<Event, EventInfo>(eve);
            }

            return null;
        }

        public void AddEvent(EventInfo eventInfo)
        {
            try
            {
                _context.Images.Add(new Image { Image1 = eventInfo.ImageUrl });
                _context.SaveChanges();
                int imageId = _context.Images.OrderBy(image => image.ImageId).LastOrDefault().ImageId;

                _context.GroupImages.Add(new GroupImage { ImageId = imageId });
                _context.SaveChanges();
                int groupId = _context.GroupImages.OrderBy(group => group.GroupImageId).LastOrDefault().GroupImageId;

                Event toAdd = _mapper.Map<EventInfo, Event>(eventInfo);
                toAdd.GroupImageId = groupId;

                _context.Events.Add(toAdd);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
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