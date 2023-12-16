using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;

namespace Ads.Persistence.Services
{
    public class SubCategoryService : SubCategoryRepository, ISubCategoryService
    {
        public SubCategoryService(AppDbContext context) : base(context)
        {
        }
    }
}
