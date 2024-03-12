using Lab7.Models;
using Lab7.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/")]
        public async Task<IActionResult> Register(User user)
        {
            await Task.Delay(2000);
            var newUser = await _authService.RegisterUser(user);
            return Ok($"User with email: {newUser.Email} registered successfully!");
        }

        [HttpPost("login/")]
        public async Task<string> Login(LoginData loginData)
        {
            await Task.Delay(2000);
            var jwt = await _authService.LoginUser(loginData.Email, loginData.PasswordHash);
            return "Bearer token: " + jwt;
        }
    }
}
