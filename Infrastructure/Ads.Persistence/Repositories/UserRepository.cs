using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ads.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetCustomList()
        {
            return await _dbSet.AsNoTracking().Include(x => x.Role).ToListAsync();
        }

        public async Task<List<User>> GetCustomList(Expression<Func<User, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(x => x.Role).ToListAsync();
        }
    }
}
