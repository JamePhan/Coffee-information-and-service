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

        [HttpGet("{count}")]
        public IActionResult List(int count)
        {
            List<UserInfo> users = _user.GetUsers(count);
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

        [Authorize(Roles = "User")]
        [HttpPatch]
        public IActionResult Update(UserInfo user)
        {
            try
            {
                _user.UpdateUser(user);
                _user.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{count}")]
        public IActionResult Banned(int count)
        {
            List<UserInfo> users = _user.GetUsersBanned(count);
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