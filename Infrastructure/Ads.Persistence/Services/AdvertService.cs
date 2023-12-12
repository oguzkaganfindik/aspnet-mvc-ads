using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class AdvertService : AdvertRepository, IAdvertService
    {
        public AdvertService(AppDbContext context) : base(context)
        {
        }
    }
}
