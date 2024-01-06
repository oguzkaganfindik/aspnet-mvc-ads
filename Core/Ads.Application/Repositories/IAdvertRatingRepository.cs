using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface IAdvertRatingRepository : IRepository<AdvertRating>
    {
        Task<List<AdvertRating>> GetCustomAdvertRatingList();
        Task<List<AdvertRating>> GetCustomAdvertRatingList(Expression<Func<AdvertRating, bool>> expression);
        Task<AdvertRating> GetCustomAdvertRating(int id);
        Task<AdvertRating> GetByUserIdAndAdvertIdAsync(int userId, int advertId);
    }
}
