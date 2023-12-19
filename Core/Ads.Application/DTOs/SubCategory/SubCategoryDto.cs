using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.SubCategoryAdvert;

namespace Ads.Application.DTOs.SubCategory
{
	public class SubCategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CategoryId { get; set; }
		public CategoryDto Category { get; set; }
		public ICollection<SubCategoryAdvertDto>? SubCategoryAdverts { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
