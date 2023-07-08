using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventController : Controller
    {
        private readonly IEventRepository _event;

        public EventController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _event = new EventRepository(context, mapper);
        }

        [HttpGet("{count}")]
        public IActionResult List(int count)
        {
            List<EventInfo> events = _event.GetEvents(count);
            if (events.Count > 0)
            {
                return Ok(events);
            }
            return NotFound();
        }

        [HttpGet("{count}")]
        public IActionResult Lastest(int count)
        {
            List<EventInfo> events = _event.GetLastest(count);
            if (events.Count > 0)
            {
                return Ok(events);
            }
            return NotFound();
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult Add(EventInfo eventInfo)
        {
            try
            {
                _event.AddEvent(eventInfo);
                _event.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            EventInfo? info = _event.GetEvent(id);
            if (info != null)
            {
                return Ok(info);
            }
            else
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _event.Dispose();
            base.Dispose(disposing);
        }
    }
}