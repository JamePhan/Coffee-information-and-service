using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScheduleController : Controller
    {
        private readonly ScheduleRepository _schedule;

        public ScheduleController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _schedule = new ScheduleRepository(context, mapper);
        }

        [HttpGet("{customerId}")]
        public IActionResult Customer(int customerId)
        {
            List<ScheduleInfo> schedules = _schedule.GetSchedulesCustomer(customerId);
            if (schedules.Count > 0)
            {
                return Ok(schedules);
            }
            return NotFound();
        }

        [HttpGet("{userId}")]
        public IActionResult User(int userId)
        {
            List<ScheduleInfo> schedules = _schedule.GetSchedulesUser(userId);
            if (schedules.Count > 0)
            {
                return Ok(schedules);
            }
            return NotFound();
        }
    }
}