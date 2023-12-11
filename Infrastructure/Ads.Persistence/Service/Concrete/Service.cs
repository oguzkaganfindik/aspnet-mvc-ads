using Ads.Domain.Entities.Common;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using Ads.Persistence.Service.Abstract;

namespace Ads.Persistence.Service.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : BaseEntity, new()
    {
        public Service(AppDbContext context) : base(context)
        {
        }
    }
}
