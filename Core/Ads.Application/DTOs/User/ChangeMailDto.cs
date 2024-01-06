using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.DTOs.User
{
    public  class ChangeMailDto
    {
      
            public string UserId { get; set; }
            public string Token { get; set; }
            public string NewEmail { get; set; }
        

    }
}
