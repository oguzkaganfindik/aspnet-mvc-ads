using Ads.Application.DTOs.Category;
using Ads.Application.Repositories;
using Ads.Application.ViewModels;

namespace Ads.Application.Services
{
    public interface ICategoryService : ICategoryRepository
    {
		Task<IEnumerable<PopularCategoryViewModel>> GetPopularCategoriesAsync();
	}
}
