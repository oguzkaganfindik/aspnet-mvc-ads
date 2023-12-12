using Ads.Application.Services;
using Ads.Domain.Entities.Common;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class Service<T> : Repository<T>, IService<T> where T : BaseEntity, new()
    {
        public Service(AppDbContext context) : base(context)
        {
        }
    }
}
