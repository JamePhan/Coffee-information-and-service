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
    public class UserController : ControllerBase
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
    }
}