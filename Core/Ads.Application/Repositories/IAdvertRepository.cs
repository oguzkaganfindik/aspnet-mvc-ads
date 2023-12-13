using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<List<Advert>> GetCustomAdvertList();
        Task<List<Advert>> GetCustomAdvertList(Expression<Func<Advert, bool>> expression);
        Task<Advert> GetCustomAdvert(int id);
    }
}
