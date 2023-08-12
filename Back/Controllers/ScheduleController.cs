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
    public class ScheduleController : Controller
    {
        private readonly ScheduleRepository _schedule;

        public ScheduleController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _schedule = new ScheduleRepository(context, mapper);
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public IActionResult Book(ScheduleInfo schedule)
        {
            try
            {
                _schedule.BookSchedule(schedule);
                _schedule.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPatch]
        public IActionResult Update(ScheduleInfo schedule)
        {
            try
            {
                _schedule.UpdateSchedule(schedule);
                _schedule.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int scheduleId)
        {
            try
            {
                _schedule.DeleteSchedule(scheduleId);
                _schedule.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}