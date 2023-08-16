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
                .Include(user => user.User)
                .Include(group => group.GroupImage)
                .ThenInclude(image => image.Image).ToList();

            return _mapper.Map<List<Event>, List<EventInfo>>(events);
        }

        public EventInfo? GetEvent(int id)
        {
            Event? eve = _context.Events.
                Include(locale => locale.Location)
                .Include(user => user.User)
                .Include(group => group.GroupImage)
                .ThenInclude(image => image.Image).FirstOrDefault(ev => ev.EventId.Equals(id));
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

                Location? location = _context.Locations.FirstOrDefault(location => location.Address.Equals(eventInfo.Address));

                int locationId;

                int userId = _context.Users.FirstOrDefault(user => user.CoffeeShopName.ToLower().Equals(eventInfo.CoffeeShopName.ToLower())).UserId;

                if (location == null)
                {
                    _context.Locations.Add(new Location
                    {
                        Address = eventInfo.Address,
                        UserId = userId,
                    });
                    _context.SaveChanges();
                    locationId = _context.Locations.OrderBy(location => location.LocationId).LastOrDefault().LocationId;
                }
                else
                {
                    locationId = location.LocationId;
                }

                Event toAdd = _mapper.Map<EventInfo, Event>(eventInfo);
                toAdd.GroupImageId = groupId;
                toAdd.LocationId = locationId;
                toAdd.UserId = userId;

                _context.Events.Add(toAdd);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateEvent(EventInfo eventInfo)
        {
            Event? checkExist = _context.Events.FirstOrDefault(ev => ev.EventId.Equals(eventInfo.EventId));
            if (checkExist != null)
            {
                try
                {
                    GroupImage? eventGroupImage = _context.GroupImages.AsNoTracking().FirstOrDefault(g => g.GroupImageId.Equals(checkExist.GroupImageId));

                    if (eventGroupImage != null)
                    {
                        Image? newsImage = _context.Images.FirstOrDefault(image => image.ImageId.Equals(eventGroupImage.ImageId));
                        if (newsImage != null) newsImage.Image1 = eventInfo.ImageUrl;
                    }

                    int userId = _context.Users.FirstOrDefault(user => user.CoffeeShopName.ToLower().Equals(eventInfo.CoffeeShopName.ToLower())).UserId;

                    Location? location = _context.Locations.FirstOrDefault(location => location.Address.Equals(eventInfo.Address));

                    int locationId;

                    if (location == null)
                    {
                        _context.Locations.Add(new Location
                        {
                            Address = eventInfo.Address,
                            UserId = userId,
                        });
                        _context.SaveChanges();
                        locationId = _context.Locations.OrderBy(location => location.LocationId).LastOrDefault().LocationId;
                    }
                    else
                    {
                        locationId = location.LocationId;
                    }

                    _context.Entry(checkExist).State = EntityState.Detached;

                    Event toUpdate = _mapper.Map<EventInfo, Event>(eventInfo);

                    toUpdate.GroupImageId = eventGroupImage.GroupImageId;
                    toUpdate.LocationId = locationId;
                    toUpdate.UserId = userId;

                    _context.Entry(toUpdate).State = EntityState.Modified;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteEvent(int id)
        {
            Event? checkExist = _context.Events.FirstOrDefault(ev => ev.EventId.Equals(id));
            if (checkExist != null)
            {
                try
                {
                    _context.Events.Remove(checkExist);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Event doesn't exist!");
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