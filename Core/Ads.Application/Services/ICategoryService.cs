using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.SubCategory;
using Ads.Application.Repositories;
using Ads.Application.ViewModels;

namespace Ads.Application.Services
{
    public interface ICategoryService : ICategoryRepository
    {
        Task CreateAsync(CategoryDto categoryDto);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task UpdateAsync(CategoryDto categoryDto);
        Task<bool> CategoryExists(int id);
        Task<List<CategoryDto>> GetAllCategories();
        Task<List<SubCategoryDto>> GetSubCategoriesByCategoryId(int categoryId);
        Task<IEnumerable<PopularCategoryViewModel>> GetPopularCategoriesAsync();
        Task DeleteAsync(int id);
        Task<AllCategoriesViewModel> GetAllCategoriesViewModelAsync();
    }
}
