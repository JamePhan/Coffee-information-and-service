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

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}