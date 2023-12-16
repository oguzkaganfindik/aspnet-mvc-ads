using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class SettingService : SettingRepository, ISettingService
    {
        public SettingService(AppDbContext context) : base(context)
        {
        }
    }
}
