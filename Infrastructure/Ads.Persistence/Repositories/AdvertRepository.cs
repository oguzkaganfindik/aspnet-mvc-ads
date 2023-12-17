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
              .Include(x => x.User)
              .Include(x => x.CategoryAdverts)
              .ThenInclude(ca => ca.Category)  // Category tablosunu dahil et
              .Include(y => y.SubCategoryAdverts)
              .ThenInclude(sa => sa.SubCategory)  // SubCategory tablosunu dahil et
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Advert>> GetCustomAdvertList()
        {
            return await _dbSet
                .Include(x => x.User)
                .Include(x => x.CategoryAdverts)
                .ThenInclude(ca => ca.Category)  // Category tablosunu dahil et
                .Include(y => y.SubCategoryAdverts)
                .ThenInclude(sa => sa.SubCategory)  // SubCategory tablosunu dahil et
                .ToListAsync();
        }

        public async Task<List<Advert>> GetCustomAdvertList(Expression<Func<Advert, bool>> expression)
        {
            return await _dbSet
               .Where(expression)
               .Include(x => x.User)
               .Include(x => x.CategoryAdverts)
               .ThenInclude(ca => ca.Category)  // Category tablosunu dahil et
               .Include(y => y.SubCategoryAdverts)
               .ThenInclude(sa => sa.SubCategory)  // SubCategory tablosunu dahil et
               .ToListAsync();
        }
    }
}
