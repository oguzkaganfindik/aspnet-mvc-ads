using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ads.Application.DTOs.AdvertRating
{
    public class AdvertRatingDto
	{
		//public int Id { get; set; }

        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int AdvertId { get; set; }
		public int? Rating { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public AdvertDto Advert { get; set; }
		public UserDto User { get; set; }

	}
}
