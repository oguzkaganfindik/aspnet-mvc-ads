using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.User;

namespace Ads.Application.DTOs.AdvertRating
{
    public class AdvertRatingDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int AdvertId { get; set; }
		public int? Rating { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public AdvertDto Advert { get; set; }
		public UserDto User { get; set; }

	}
}
