using Lab7.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab7.Services
{
    public class AuthService
    {
        protected UserService _userService;
        protected PasswordService _passwordService;

        public AuthService(UserService userService, PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        public async Task<User> RegisterUser(User user)
        {
            var existingUser = await _userService.ValidateUser(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists!");
            }

            user.Id = _userService._users.Max(u => u.Id) + 1;
            user.PasswordHash = _passwordService.HashPassword(user.PasswordHash);
            _userService._users.Add(user);
            return user;
        }

        public async Task<string> LoginUser(string email, string password)
        {
            var existingUser = await _userService.ValidateUser(email);
            if (existingUser == null)
            {
                return "Invalid Email";
            }

            if (_passwordService.ConfirmPassword(password, existingUser.PasswordHash))
            {
                var token = JwtToken(existingUser);
                return token;
            }

            existingUser.LastLoginDate = DateTime.Now;
            existingUser.FailedLoginAttempts++;      

            return "Password is incorrect! Please try again";
        }

        private static string JwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASuperSecretKeyForJWTTokenGeneration"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyAppIssuer",
                audience: "MyAppAudience",
                claims: claims,
                expires: DateTime.Now.AddDays(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
