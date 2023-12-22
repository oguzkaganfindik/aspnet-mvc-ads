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

        public async Task<User> GetCustomUser(int id)
        {
            return await _dbSet.Include(x => x.Role)
                        .Include(u => u.Adverts)
        .Include(u => u.AdvertComments)
        .Include(u => u.AdvertRatings)
            .ThenInclude(ar => ar.Advert)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<User>> GetCustomList()
        {
            return await _dbSet.Include(x => x.Role)
                        .Include(u => u.Adverts)
        .Include(u => u.AdvertComments)
        .Include(u => u.AdvertRatings)
            .ThenInclude(ar => ar.Advert)
                .ToListAsync();
        }

        public async Task<List<User>> GetCustomList(Expression<Func<User, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(x => x.Role)
                        .Include(u => u.Adverts)
        .Include(u => u.AdvertComments)
        .Include(u => u.AdvertRatings)
            .ThenInclude(ar => ar.Advert)
                .ToListAsync();
        }
    }
}
