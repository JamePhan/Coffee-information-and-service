﻿using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            ServiceInfo? service = _service.GetService(id);
            if (service != null)
            {
                return Ok(service);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            List<ServiceInfo> services = _service.GetServices();
            if (services.Count > 0)
            {
                return Ok(services);
            }
            return NotFound();
        }

        //[Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Add(ServiceInfo service)
        {
            try
            {
                _service.AddService(service);
                _service.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult Update(ServiceInfo service)
        {
            try
            {
                _service.UpdateService(service);
                _service.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "User")]
        [HttpDelete("{serviceId}")]
        public IActionResult Delete(int serviceId)
        {
            try
            {
                _service.RemoveService(serviceId);
                _service.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult User(int userId)
        {
            List<ServiceInfo> services = _service.GetUserServices(userId);
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