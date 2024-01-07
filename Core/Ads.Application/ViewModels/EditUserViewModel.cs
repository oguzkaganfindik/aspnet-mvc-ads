using Ads.Application.DTOs.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.ViewModels
{
    public class EditUserViewModel
    {
        public UserEditDto UserEditDto { get; set; }

        public IFormFile? File { get; set; }
    }
}
