using Ads.Application.Repositories;
using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using Ads.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ads.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConStr")));

            services.AddTransient(typeof(IService<>), typeof(Service<>));
            services.AddTransient<IAdvertService, AdvertService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IAdvertCommentService, AdvertCommentService>();
            services.AddTransient<IAdvertImageService, AdvertImageService>();
            services.AddTransient<IAdvertRatingService, AdvertRatingService>();
            services.AddScoped<INavbarService, NavbarService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IAdvertImageRepository, AdvertImageRepository>();
            services.AddScoped<IAdvertRatingRepository, AdvertRatingRepository>();
            services.AddScoped<IAdvertCommentRepository, AdvertCommentRepository>();
        }
    }
}