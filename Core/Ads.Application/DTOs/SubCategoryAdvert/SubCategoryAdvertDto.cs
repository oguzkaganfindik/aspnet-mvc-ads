using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.SubCategory;

namespace Ads.Application.DTOs.SubCategoryAdvert
{
    public class SubCategoryAdvertDto
	{
		public int Id { get; set; }
		public int SubCategoryId { get; set; }
		public int AdvertId { get; set; }
		public AdvertDto Advert { get; set; }
		public SubCategoryDto SubCategory { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
