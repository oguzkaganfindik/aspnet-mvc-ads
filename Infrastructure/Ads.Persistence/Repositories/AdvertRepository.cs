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
            return await _dbSet.Include(x => x.CategoryAdverts).Include(y => y.SubCategoryAdverts).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Advert>> GetCustomAdvertList()
        {
            return await _dbSet.Include(x => x.CategoryAdverts).Include(y => y.SubCategoryAdverts).ToListAsync();
        }

        public async Task<List<Advert>> GetCustomAdvertList(Expression<Func<Advert, bool>> expression)
        {
            return await _dbSet.Where(expression).Include(x => x.CategoryAdverts).Include(y => y.SubCategoryAdverts).ToListAsync();
        }
    }
}
