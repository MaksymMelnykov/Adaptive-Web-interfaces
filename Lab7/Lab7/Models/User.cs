using System.ComponentModel.DataAnnotations;

namespace Lab7.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(15, ErrorMessage = "FirstName cannot be longer than 15 characters")]
        public string FirstName { get; set; }
        [StringLength(15, ErrorMessage = "LastName cannot be longer than 15 characters")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int FailedLoginAttempts { get; set; }
    }
}
