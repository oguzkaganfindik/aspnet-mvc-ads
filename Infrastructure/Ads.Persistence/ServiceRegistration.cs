using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ads.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddTransient(typeof(IService<>), typeof(Service<>));
            services.AddTransient<IAdvertService, AdvertService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IAdvertCommentService, AdvertCommentService>();
            services.AddTransient<IAdvertImageService, AdvertImageService>();
            services.AddTransient<IAdvertRatingService, AdvertRatingService>();
            services.AddScoped<INavbarService, NavbarService>();
        }
    }
}