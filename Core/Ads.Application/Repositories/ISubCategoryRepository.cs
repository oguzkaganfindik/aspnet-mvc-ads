using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<List<SubCategory>> GetCustomSubCategoryList();
        Task<List<SubCategory>> GetCustomSubCategoryList(Expression<Func<SubCategory, bool>> expression);
        Task<SubCategory> GetCustomSubCategory(int id);
    }
}
