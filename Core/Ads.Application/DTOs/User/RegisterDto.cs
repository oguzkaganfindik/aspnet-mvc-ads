using System.ComponentModel.DataAnnotations;

namespace Ads.Application.DTOs.User
{
    public class RegisterDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

    }
}
