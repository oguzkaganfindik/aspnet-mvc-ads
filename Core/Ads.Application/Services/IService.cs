using Ads.Application.Repositories;
using Ads.Domain.Entities.Abstract;

namespace Ads.Application.Services
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, IAuiditEntity, new()
    {
    }
}
