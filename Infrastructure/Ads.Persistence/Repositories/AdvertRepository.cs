using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ads.Persistence.Repositories
{
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        public AdvertRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Advert> GetCustomAdvert(int id)
        {
            return await _dbSet
            .Include(a => a.User)
            .Include(b => b.CategoryAdverts)
            .ThenInclude(b => b.Category)
            .Include(s => s.SubCategoryAdverts)
            .ThenInclude(s => s.SubCategory)
            .Include(i => i.AdvertImages)
            .Include(r => r.AdvertRatings)
            .ThenInclude(r => r.User)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Advert>> GetCustomAdvertList()
        {
            return await _dbSet
           .Include(a => a.User)
           .Include(b => b.CategoryAdverts)
           .ThenInclude(b => b.Category)
           .Include(s => s.SubCategoryAdverts)
           .ThenInclude(s => s.SubCategory)
           .Include(i => i.AdvertImages)
           .Include(r => r.AdvertRatings)
           .ThenInclude(r => r.User)
                 .ToListAsync();
        }

        public async Task<List<Advert>> GetCustomAdvertList(Expression<Func<Advert, bool>> expression)
        {
            return await _dbSet
            .Include(a => a.User)
            .Include(b => b.CategoryAdverts)
            .ThenInclude(b => b.Category)
            .Include(s => s.SubCategoryAdverts)
            .ThenInclude(s => s.SubCategory)
            .Include(i => i.AdvertImages)
            .Include(r => r.AdvertRatings)
            .ThenInclude(r => r.User)
        .ToListAsync();
        }
    }
}
