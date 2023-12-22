using Ads.Application.DTOs.CategoryAdvert;
using Ads.Application.DTOs.SubCategory;

namespace Ads.Application.DTOs.Category
{
    public class CategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CategoryIconPath { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public ICollection<CategoryAdvertDto> CategoryAdverts { get; set; }
		public ICollection<SubCategoryDto> SubCategories { get; set; }
	}
}
