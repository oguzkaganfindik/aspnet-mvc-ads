using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.DTOs.Role;
using Ads.Application.DTOs.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.DTOs.User
{
    public class UserEditDto : BaseUserDto
    {
        public int Id { get; set; }
        public string? UserImagePath { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public int SettingId { get; set; }
        public int? RoleId { get; set; }
        public virtual SettingDto? Setting { get; set; }
        public bool UserTheme { get; set; }
    }
}
