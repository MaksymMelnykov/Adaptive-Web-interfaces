using Lab7.Models;
using Lab7.Services.Interfaces;
using Serilog;

namespace Lab7.Services
{
    public class UserService : IUserService
    {
        private readonly Serilog.ILogger _logger = Log.ForContext<UserService>();
        protected PasswordService _passwordService = new PasswordService();
        public List<User> _users;

        public UserService()
        {
            _users = new List<User>
            {
                new User { Id = 1, FirstName = "Maksym", LastName = "Melnykov", Email = "melnikovmaks1202@gmail.com", DateOfBirth = new DateTime(2004, 2, 12), PasswordHash = _passwordService.HashPassword("password123"), LastLoginDate = DateTime.Now, FailedLoginAttempts = 0 },
                new User { Id = 2, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", DateOfBirth = new DateTime(1990, 5, 15), PasswordHash = _passwordService.HashPassword("qwerty123"), LastLoginDate = DateTime.Now.AddDays(-7), FailedLoginAttempts = 0 },
                new User { Id = 3, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@outlook.com", DateOfBirth = new DateTime(1985, 11, 22), PasswordHash = _passwordService.HashPassword("password456"), LastLoginDate = DateTime.Now.AddMonths(-1), FailedLoginAttempts = 0 },
                new User { Id = 4, FirstName = "Mykhaylo", LastName = "Shevchenko", Email = "shevchenko@gmail.com", DateOfBirth = new DateTime(1978, 7, 3), PasswordHash = _passwordService.HashPassword("securepassword"), LastLoginDate = DateTime.Now.AddYears(-2), FailedLoginAttempts = 0 },
                new User { Id = 5, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@hmail.com", DateOfBirth = new DateTime(1995, 3, 18), PasswordHash = _passwordService.HashPassword("password789"), LastLoginDate = DateTime.Now.AddDays(-14), FailedLoginAttempts = 0 },
                new User { Id = 6, FirstName = "Daniel", LastName = "Ivanov", Email = "danielivanov@ukr.net", DateOfBirth = new DateTime(1988, 9, 28), PasswordHash = _passwordService.HashPassword("mypassword123"), LastLoginDate = DateTime.Now.AddMonths(-3), FailedLoginAttempts = 0 },
                new User { Id = 7, FirstName = "Yulia", LastName = "Tkachenko", Email = "tkachenko93@gmail.com", DateOfBirth = new DateTime(1993, 6, 10), PasswordHash = _passwordService.HashPassword("password123!@#"), LastLoginDate = DateTime.Now.AddDays(-21), FailedLoginAttempts = 0 },
                new User { Id = 8, FirstName = "Sergiy", LastName = "Kravchenko", Email = "sergiy.krava@outlook.com", DateOfBirth = new DateTime(1980, 12, 5), PasswordHash = _passwordService.HashPassword("password456!@#"), LastLoginDate = DateTime.Now.AddMonths(-6), FailedLoginAttempts = 0 },
                new User { Id = 9, FirstName = "Sophia", LastName = "Melnichuk", Email = "sophia20@outlook.com", DateOfBirth = new DateTime(1998, 8, 20), PasswordHash = _passwordService.HashPassword("mysecurepassword"), LastLoginDate = DateTime.Now.AddDays(-2), FailedLoginAttempts = 0 },
                new User { Id = 10, FirstName = "Andriy", LastName = "Bondarenko", Email = "bondarenko.andriy@ukr.net", DateOfBirth = new DateTime(1975, 4, 12), PasswordHash = _passwordService.HashPassword("password123456"), LastLoginDate = DateTime.Now.AddYears(-1), FailedLoginAttempts = 0 }
            };
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            _logger.Information("Getting all users");

            return _users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            _logger.Information("Attempting to get user by id: {UserId}", id);

            var user = _users.FirstOrDefault(user => user.Id == id);

            if (user != null)
            {
                _logger.Information("User found: {@User}", user);
            }
            else
            {
                _logger.Warning("User with id {UserId} not found", id);
            }

            return user;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _logger.Information("Adding a new user: {@User}", user);

            user.Id = _users.Max(u => u.Id) + 1;
            user.PasswordHash = (new PasswordService()).HashPassword(user.PasswordHash);
            _users.Add(user);

            _logger.Information("User added successfully");

            return user;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            _logger.Information("Attempting to update user with id: {UserId}", id);

            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.PasswordHash = user.PasswordHash;

                _logger.Information("User updated successfully: {@User}", existingUser);

                return existingUser;
            }
            else
            {
                _logger.Warning("User with id {UserId} not found", id);
                return null;
            }
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            _logger.Information("Attempting to delete user with id: {UserId}", id);

            var userToRemove = _users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);

                _logger.Information("User deleted successfully: {@User}", userToRemove);

                return userToRemove;
            }
            else
            {
                _logger.Warning("User with id {UserId} not found", id);
                return null;
            }
        }

        public async Task<User> ValidateUser(string email)
        {
            _logger.Information("Attempting to validate user with email: {UserEmail}", email);

            var existingUser = _users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                _logger.Information("User validated successfully: {@User}", existingUser);
                return existingUser;
            }
            else
            {
                _logger.Warning("User with email {UserEmail} not found", email);
                return null;
            }
        }
    }
}
