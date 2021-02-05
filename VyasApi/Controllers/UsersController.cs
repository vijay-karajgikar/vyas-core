using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VyasApi.Data.Dto;

namespace VyasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            return Ok(new { operation = "create user" });
        }

		[Route("")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(new { operation = "get users" });
        }

		[Route("{id}")]
        [HttpGet]
        public IActionResult GetUser([FromQuery] int? userId)
        {
            return Ok(new { operation = "get user by id" });
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDto user)
        {
            return Ok(new { operation = "update user" });
        }

		[Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int? userId)
        {
            return Ok(new { operation = "delete user" });
        }
    }
}