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

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult List()
        {
            List<CustomerInfo> customers = _customer.GetCustomers();
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

        //[Authorize(Roles = "Customer")]
        [HttpPut]
        public IActionResult Update(CustomerInfo customer)
        {
            try
            {
                int accountId = _customer.GetCustomerByEmail(customer.Email).AccountId.Value;
                _customer.UpdateCustomer(customer, accountId);
                _customer.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Banned()
        {
            List<CustomerInfo> customers = _customer.GetCustomersBanned();
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