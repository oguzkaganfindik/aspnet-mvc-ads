using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.DTOs.User
{
    public class ResetPasswordDto
    {
       
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
    }
}
