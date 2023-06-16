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
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _service;

        public ServiceController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _service = new ServiceRepository(context, mapper);
        }

        [HttpGet("{count}")]
        public IActionResult List(int count)
        {
            List<ServiceInfo> services = _service.GetServices(count);
            if (services.Count > 0)
            {
                return Ok(services);
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult Add(ServiceInfo service)
        {
            try
            {
                _service.AddService(service);
                _service.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public IActionResult Update(ServiceInfo service)
        {
            try
            {
                _service.UpdateService(service);
                _service.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}