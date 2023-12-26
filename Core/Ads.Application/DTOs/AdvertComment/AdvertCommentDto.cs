using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace Ads.Application.DTOs.AdvertComment
{
    public class AdvertCommentDto
    {
        public int Id { get; set; }

        [StringLength(500, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(10, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Comment { get; set; }

        public bool IsActive { get; set; }

        public string IsActiveString => IsActive ? "Active" : "Passive";

        public AdvertDto Advert { get; set; }
        public int AdvertId { get; set; }

        public UserDto User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
