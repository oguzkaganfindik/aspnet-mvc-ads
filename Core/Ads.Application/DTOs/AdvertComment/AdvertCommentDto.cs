using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.User;

namespace Ads.Application.DTOs.AdvertComment
{
    public class AdvertCommentDto
	{
		public int Id { get; set; }
		public string Comment { get; set; }
		public bool IsActive { get; set; }
		public AdvertDto Advert { get; set; }
		public int AdvertId { get; set; }
		public UserDto User { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
