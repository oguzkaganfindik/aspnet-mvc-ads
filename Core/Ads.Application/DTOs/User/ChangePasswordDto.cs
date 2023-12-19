using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.DTOs.User
{
	public class ChangePasswordDto
	{
		public int UserId { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}