using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using Ads.Persistence.Service.Abstract;

namespace Ads.Persistence.Service.Concrete
{
    public class AdvertService : AdvertRepository, IAdvertService
    {
        public AdvertService(AppDbContext context) : base(context)
        {
        }
    }
}
