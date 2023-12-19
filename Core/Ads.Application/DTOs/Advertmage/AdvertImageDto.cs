using Ads.Application.DTOs.Advert;

namespace Ads.Application.DTOs.Advertmage
{
	public class AdvertImageDto
	{
		public int Id { get; set; }
		public string AdvertImagePath { get; set; }
		public int AdvertId { get; set; }
		public AdvertDto Advert { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
