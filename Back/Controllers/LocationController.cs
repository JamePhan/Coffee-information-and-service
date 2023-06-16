using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _location;

        public LocationController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _location = new LocationRepository(context, mapper);
        }

        [HttpGet("{count}")]
        public IActionResult List(int count)
        {
            List<LocationInfo> locations = _location.GetLocations(count);
            if (locations.Count > 0)
            {
                return Ok(locations);
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult Add(LocationInfo location)
        {
            try
            {
                _location.AddLocation(location);
                _location.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _location.RemoveLocation(id);
                _location.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _location.Dispose();
            base.Dispose(disposing);
        }
    }
}