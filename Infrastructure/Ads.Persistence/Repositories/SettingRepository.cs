using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ads.Persistence.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Setting> GetCustomSetting(int id)
        {
            return await _dbSet
                .Include(x => x.Pages)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Setting>> GetCustomSettingList()
        {
            return await _dbSet
                .Include(x => x.Pages)
                .ToListAsync();
        }

        public async Task<List<Setting>> GetCustomSettingList(Expression<Func<Setting, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(x => x.Pages)
                .ToListAsync();
        }
    }
}
