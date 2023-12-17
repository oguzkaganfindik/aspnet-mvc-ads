using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class AdvertRatingService : AdvertRatingRepository, IAdvertRatingService
    {
        public AdvertRatingService(AppDbContext context) : base(context)
        {
        }
    }
}
