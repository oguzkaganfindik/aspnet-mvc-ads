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
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        public AdvertRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Advert> GetCustomAdvert(int id)
        {
            return await _dbSet.AsNoTracking().Include(x => x.CategoryAdverts).Include(y => y.SubCategoryAdverts).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Advert>> GetCustomAdvertList()
        {
            return await _dbSet.AsNoTracking().Include(x => x.CategoryAdverts).Include(y => y.SubCategoryAdverts).ToListAsync();
        }

        public async Task<List<Advert>> GetCustomAdvertList(Expression<Func<Advert, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(x => x.CategoryAdverts).Include(y => y.SubCategoryAdverts).ToListAsync();
        }
    }
}
