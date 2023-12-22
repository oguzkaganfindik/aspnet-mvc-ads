using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ads.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetCustomCategory(int id)
        {
            return await _dbSet
                .Include(x => x.SubCategories)
                .Include(y => y.CategoryAdverts)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetCustomCategoryList()
        {
            return await _dbSet
                .Include(x => x.SubCategories)
                .Include(y => y.CategoryAdverts)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCustomCategoryList(Expression<Func<Category, bool>> expression)
        {
            return await _dbSet.Where(expression)
                .Include(x => x.SubCategories)
                .Include(y => y.CategoryAdverts)
                .ToListAsync();
        }
    }
}