using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ads.Persistence.Repositories
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Page> GetCustomPage(int id)
        {
            return await _dbSet
                .Include(x => x.Setting)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Page>> GetCustomPageList()
        {
            return await _dbSet
                .Include(x => x.Setting)
                .ToListAsync();
        }

        public async Task<List<Page>> GetCustomPageList(Expression<Func<Page, bool>> expression)
        {
            return await _dbSet.Where(expression)
                 .Include(x => x.Setting)
                 .ToListAsync();
        }
    }
}
