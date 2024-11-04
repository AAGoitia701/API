using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
