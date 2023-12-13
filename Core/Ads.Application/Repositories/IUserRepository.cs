using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetCustomList();
        Task<List<User>> GetCustomList(Expression<Func<User, bool>> expression);
    }
}
