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
    public class FollowController : Controller
    {
        private readonly IFollowRepository _follow;

        public FollowController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _follow = new FollowRepository(context, mapper);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{customerId}")]
        public IActionResult CustomerList(int customerId)
        {
            List<FollowInfo> following = _follow.GetFollowingUsers(customerId);
            if (following.Count > 0)
            {
                return Ok(following);
            }
            return NotFound();
        }

        [Authorize(Roles = "User")]
        [HttpGet("{userId}")]
        public IActionResult UserList(int userId)
        {
            List<FollowInfo> followings = _follow.GetFollowingUsers(userId);
            if (followings.Count > 0)
            {
                return Ok(followings);
            }
            return NotFound();
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public IActionResult Follow(FollowInfo follow)
        {
            try
            {
                _follow.AddFollow(follow);
                _follow.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _follow.Dispose();
            base.Dispose(disposing);
        }
    }
}