using Lab7.Models;
using Lab7.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users/")]
        public async Task<IActionResult> GetUsers()
        {
            await Task.Delay(2000);
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            await Task.Delay(2000);
            var users = await _userService.GetUserByIdAsync(id);
            if (users == null)
                return NotFound($"User with ID {id} not found");
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await Task.Delay(2000);
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            await Task.Delay(2000);
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
                return NotFound($"User with ID {id} not found");
            return Ok($"User with ID {id} updated successfully");
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await Task.Delay(2000);
            var deletedUser = await _userService.DeleteUserAsync(id);
            if (deletedUser == null)
                return NotFound($"User with ID {id} not found");
            return Ok($"User with ID {id} deleted successfully");
        }
    }
}
