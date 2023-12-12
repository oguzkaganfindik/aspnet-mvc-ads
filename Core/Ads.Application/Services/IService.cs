using Ads.Application.Repositories;
using Ads.Domain.Entities.Common;

namespace Ads.Application.Services
{
    public interface IService<T> : IRepository<T> where T : BaseEntity, new()
    {
    }
}
