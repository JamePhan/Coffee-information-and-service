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

        //[Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Add(EventInfo eventInfo)
        {
            try
            {
                _event.AddEvent(eventInfo);
                _event.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPut]
        public IActionResult Update(EventInfo eventInfo)
        {
            try
            {
                _event.UpdateEvent(eventInfo);
                _event.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _event.DeleteEvent(id);
                _event.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _event.Dispose();
            base.Dispose(disposing);
        }
    }
}