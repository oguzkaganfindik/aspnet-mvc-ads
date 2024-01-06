using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.Repositories
{
    public class AdvertRatingRepository : Repository<AdvertRating>, IAdvertRatingRepository
    {
        public AdvertRatingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AdvertRating> GetCustomAdvertRating(int id)
        {
            return await _dbSet
              .Include(x => x.User)
              .Include(x => x.Advert)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<AdvertRating>> GetCustomAdvertRatingList()
        {
            return await _dbSet
              .Include(x => x.User)
              .Include(x => x.Advert)
                .ToListAsync();
        }
        public async Task<List<AdvertRating>> GetCustomAdvertRatingList(Expression<Func<AdvertRating, bool>> expression)
        {
            return await _dbSet
                .Where(expression)
                .Include(x => x.User)
                .Include(x => x.Advert)
                .ToListAsync();
        }
        public async Task<AdvertRating> GetByUserIdAndAdvertIdAsync(int userId, int advertId)
        {
            return await _context.Set<AdvertRating>()
                                 .FirstOrDefaultAsync(ar => ar.UserId == userId && ar.AdvertId == advertId);
        }

    }
}
