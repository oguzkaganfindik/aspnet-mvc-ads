using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;

namespace Ads.Application.Services
{
    public interface IAdvertService : IAdvertRepository
    {
        Task<List<Advert>> Search(string query, int page, int pageSize);
    }
}
