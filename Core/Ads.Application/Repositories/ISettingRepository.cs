using Ads.Domain.Entities.Concrete;
using System.Linq.Expressions;

namespace Ads.Application.Repositories
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Task<List<Setting>> GetCustomSettingList();
        Task<List<Setting>> GetCustomSettingList(Expression<Func<Setting, bool>> expression);
        Task<Setting> GetCustomSetting(int id);
    }
}
