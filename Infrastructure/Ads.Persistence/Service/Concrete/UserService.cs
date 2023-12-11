using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using Ads.Persistence.Service.Abstract;

namespace Ads.Persistence.Service.Concrete
{
    public class UserService : UserRepository, IUserService
    {
        public UserService(AppDbContext context) : base(context)
        {
        }

    }
}
