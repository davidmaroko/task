using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RonnieProjects.Entities;
using RonnieProjects.UserHandler;
using RonnieProjects.Storage;

namespace UserApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController()
        {
            var dapperFactory = new SqlConnectionFactory();
            _userService = new UserService(dapperFactory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([Bind("Id,FirstName,LastName")] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user data.");
                }

                var newUserId = await _userService.AddUserAsync(user);
                return StatusCode(201, new { Id = newUserId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
