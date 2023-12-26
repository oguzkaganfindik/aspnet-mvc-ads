using Ads.Application.DTOs.Advert;
using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;

namespace Ads.Application.Services
{
    public interface IAdvertService : IAdvertRepository
    {
        Task<IEnumerable<AdvertDto>> Search(string query, int page, int pageSize);
        Task<IEnumerable<AdvertDto>> GetTrendingAddsAsync();
    }
}
