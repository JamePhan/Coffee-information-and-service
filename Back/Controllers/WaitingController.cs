using AutoMapper;
using Library.DAL;
using Library.DAL.Interfaces;
using Library.DAL.Repositories;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WaitingController : Controller
    {
        private readonly IWaitingRepository _waiting;
        private readonly ICustomerRepository _customer;
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;

        public WaitingController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _waiting = new WaitingRepository(context, mapper);
            _customer = new CustomerRepository(context, mapper);
            _user = new UserRepository(context, mapper);
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<WaitingInfo> info = _waiting.GetWaitings();
            return Ok(info);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult Request(int customerId)
        {
            try
            {
                _waiting.AddWaiting(customerId);
                _waiting.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Accept(int id)
        {
            WaitingInfo? waitInfo = _waiting.GetWaiting(id);
            if (waitInfo == null) { return NotFound("Waiting record doesn't exist!"); }

            try
            {
                int accountId = _customer.GetCustomer(waitInfo.CustomerId.Value).AccountId.Value;

                UserInfo uinfo = _mapper.Map<WaitingInfo, UserInfo>(waitInfo);
                uinfo.AccountId = accountId;

                _user.AddUser(uinfo);
                _user.Save();

                _customer.DeleteCustomer(waitInfo.CustomerId.Value);
                _customer.Save();

                _waiting.RemoveWaiting(id);
                _waiting.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Deny(int id)
        {
            try
            {
                _waiting.RemoveWaiting(id);
                _waiting.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}