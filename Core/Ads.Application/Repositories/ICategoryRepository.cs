using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetCustomCategoryList();
        Task<List<Category>> GetCustomCategoryList(Expression<Func<Category, bool>> expression);
        Task<Category> GetCustomCategory(int id);
    }

}
