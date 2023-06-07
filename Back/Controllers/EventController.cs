using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _event;

        public EventController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _event = new EventRepository(context, mapper);
        }

        [HttpGet]
        public IActionResult List()
        {
            List<EventInfo> events = _event.GetEvents();
            if (events.Count > 0)
            {
                return Ok(events);
            }
            return NotFound();
        }
    }
}