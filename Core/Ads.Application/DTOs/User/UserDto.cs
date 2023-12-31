using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.DTOs.Role;
using Ads.Application.DTOs.Setting;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserImagePath { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }

        public ICollection<AdvertDto>? Adverts { get; set; }
        public ICollection<AdvertCommentDto>? AdvertComments { get; set; }
        public ICollection<AdvertRatingDto>? AdvertRatings { get; set; }


        public int? SettingId { get; set; }
        public virtual SettingDto? Setting { get; set; }
        public virtual RoleDto? Role { get; set; }
        public int? RoleId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public Guid? UserGuid { get; set; }
    }
}