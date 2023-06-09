﻿using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customer;

        public CustomerController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _customer = new CustomerRepository(context, mapper);
        }

        [HttpGet("{count}")]
        public IActionResult List(int count)
        {
            List<CustomerInfo> customers = _customer.GetCustomers(count);
            if (customers.Count > 0)
            {
                return Ok(customers);
            }
            return NotFound();
        }

        [HttpGet("{count}")]
        public IActionResult Banned(int count)
        {
            List<CustomerInfo> customers = _customer.GetCustomersBanned(count);
            if (customers.Count > 0)
            {
                return Ok(customers);
            }
            return NotFound();
        }
    }
}