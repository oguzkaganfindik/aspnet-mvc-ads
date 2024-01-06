using Ads.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.DTOs.User
{
    public class BaseUserDto
    {
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
