using Ads.Application.Services;
using Ads.Domain.Entities.Abstract;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, IAuiditEntity, new()
    {
        public Service(AppDbContext context) : base(context)
        {
        }
    }
}
