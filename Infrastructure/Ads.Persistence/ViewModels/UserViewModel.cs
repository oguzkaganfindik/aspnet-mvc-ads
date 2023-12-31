using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.ViewModels
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? PictureUrl { get; set; }

    }
}
