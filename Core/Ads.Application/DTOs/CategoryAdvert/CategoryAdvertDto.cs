using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.Category;

namespace Ads.Application.DTOs.CategoryAdvert
{
	public class CategoryAdvertDto
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public int AdvertId { get; set; }
		public AdvertDto Advert { get; set; }
		public CategoryDto Category { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
