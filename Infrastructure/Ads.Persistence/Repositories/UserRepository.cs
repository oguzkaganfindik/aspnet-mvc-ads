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
            return await _dbSet.Include(x => x.Role)
                .Include(x => x.Setting)
                .ToListAsync();
        }

        public async Task<List<User>> GetCustomList(Expression<Func<User, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(x => x.Role)
                .Include(x => x.Setting)
                .ToListAsync();
        }
    }
}
