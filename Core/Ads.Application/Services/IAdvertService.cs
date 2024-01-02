using Ads.Application.DTOs.Advert;
using Ads.Application.Repositories;

namespace Ads.Application.Services
{
    public interface IAdvertService : IAdvertRepository
    {
        Task<IEnumerable<AdvertDto>> Search(string query, int page, int pageSize);
        Task<IEnumerable<AdvertDto>> GetTrendingAddsAsync();
    }
}
