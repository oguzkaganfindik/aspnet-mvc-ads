using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ads.Persistence.Repositories
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<SubCategory> GetCustomSubCategory(int id)
        {
            return await _dbSet
                .Include(x => x.Category) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<SubCategory>> GetCustomSubCategoryList()
        {
            return await _dbSet
                .Include(x => x.Category)  
                .ToListAsync();
        }

        public async Task<List<SubCategory>> GetCustomSubCategoryList(Expression<Func<SubCategory, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(x => x.Category) 
                .ToListAsync();
        }
    }
    
}
