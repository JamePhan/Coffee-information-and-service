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
    public class UserController : Controller
    {
        private readonly IUserRepository _user;

        public UserController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _user = new UserRepository(context, mapper);
        }

        [HttpGet]
        public IActionResult List()
        {
            List<UserInfo> users = _user.GetUsers();
            if (users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound();
        }

        [HttpGet("{name}")]
        public IActionResult Search(string name)
        {
            List<UserInfo> users = _user.GetUsers(name);
            if (users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound();
        }

        //[Authorize(Roles = "User")]
        [HttpPatch]
        public IActionResult Update(UserInfo user)
        {
            try
            {
                int accountId = _user.GetUserByEmail(user.Email).AccountId.Value;
                _user.UpdateUser(user, accountId);
                _user.Save();
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
            List<UserInfo> users = _user.GetUsersBanned();
            if (users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            _user.Dispose();
            base.Dispose(disposing);
        }
    }
}