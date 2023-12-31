using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<List<Page>> GetCustomPageList();
        Task<List<Page>> GetCustomPageList(Expression<Func<Page, bool>> expression);
        Task<Page> GetCustomPage(int id);
    }
}
