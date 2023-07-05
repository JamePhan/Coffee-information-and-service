using AutoMapper;
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
    public class CustomerController : Controller
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

        [HttpGet("{name}")]
        public IActionResult Search(string name)
        {
            List<CustomerInfo> customers = _customer.GetCustomers(name);
            if (customers.Count > 0)
            {
                return Ok(customers);
            }
            return NotFound();
        }

        [HttpPatch]
        public IActionResult Update(CustomerInfo customer)
        {
            try
            {
                _customer.UpdateCustomer(customer);
                _customer.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
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

        protected override void Dispose(bool disposing)
        {
            _customer.Dispose();
            base.Dispose(disposing);
        }
    }
}