using System.ComponentModel.DataAnnotations;

namespace Lab7.Models
{
    public class LoginData
    {
        [EmailAddress] 
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
