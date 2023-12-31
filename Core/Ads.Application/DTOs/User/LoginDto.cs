using System.ComponentModel.DataAnnotations;

namespace Ads.Application.DTOs.User
{
    public class LoginDto
    {

        public string Email { get; set; }


        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string UserName { get; set; }
    }
}
