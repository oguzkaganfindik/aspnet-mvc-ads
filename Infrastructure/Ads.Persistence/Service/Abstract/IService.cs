using Ads.Application.Repositories;
using Ads.Domain.Entities.Common;

namespace Ads.Persistence.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : BaseEntity, new()
    {
    }
}
