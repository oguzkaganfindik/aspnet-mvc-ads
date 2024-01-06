using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.Repositories;

namespace Ads.Application.Services
{
    public interface IAdvertService : IAdvertRepository
    {
        Task<List<AdvertDto>> GetAllAdvertsAsync();
        Task<AdvertDto> GetAdvertByIdAsync(int advertId);
        Task<AdvertDto> GetAdvertDetailsAsync(int advertId);
        Task AddAdvertAsync(AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds);
        Task<IEnumerable<AdvertDto>> Search(string query, int page, int pageSize);
        Task<IEnumerable<AdvertDto>> GetTrendingAddsAsync();
        Task DeleteAsync(int id);
        Task UpdateAdvertAsync(AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds);
        Task<IEnumerable<AdvertRatingDto>> GetAdvertRatings(int advertId);
    }
}
