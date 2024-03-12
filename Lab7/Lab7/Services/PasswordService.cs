using System.Security.Cryptography;
using System.Text;

namespace Lab7.Services
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public bool ConfirmPassword(string password, string hashPassword)
        {
            return HashPassword(password) == hashPassword;
        }
    }
}
