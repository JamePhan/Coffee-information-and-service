using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class EventRepository : IEventRepository
    {
        private CoffeehouseSystemContext _context;

        public EventRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public List<Event> GetEvents()
        {
            return _context.Events.ToList();
        }
    }
}