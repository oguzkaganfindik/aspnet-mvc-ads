using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface IAdvertImageRepository : IRepository<AdvertImage>
    {
        Task<List<AdvertImage>> GetCustomAdvertImageList();
        Task<List<AdvertImage>> GetCustomAdvertImageList(Expression<Func<AdvertImage, bool>> expression);
        Task<AdvertImage> GetCustomAdvertImage(int id);
    }
}
