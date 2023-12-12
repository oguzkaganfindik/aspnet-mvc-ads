using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class UserService : UserRepository, IUserService
    {
        public UserService(AppDbContext context) : base(context)
        {
        }

    }
}
