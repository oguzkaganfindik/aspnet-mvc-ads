using Ads.Application.Repositories;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ads.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            //services.AddScoped<IAdvertReadRepository, AdvertReadRepository>();
            //services.AddScoped<IAdvertWriteRepository, AdvertWriteRepository>();
            //services.AddScoped<IAdvertCommentReadRepository, AdvertCommentReadRepository>();
            //services.AddScoped<IAdvertCommentWriteRepository, AdvertCommentWriteRepository>();
            //services.AddScoped<IAdvertImageReadRepository, AdvertImageReadRepository>();
            //services.AddScoped<IAdvertImageWriteRepository, AdvertImageWriteRepository>();
            //services.AddScoped<IAdvertRatingReadRepository, AdvertRatingReadRepository>();
            //services.AddScoped<IAdvertRatingWriteRepository, AdvertRatingWriteRepository>();
            //services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            //services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            //services.AddScoped<ICategoryAdvertReadRepository, CategoryAdvertReadRepository>();
            //services.AddScoped<ICategoryAdvertWriteRepository, CategoryAdvertWriteRepository>();
            //services.AddScoped<IPageReadRepository, PageReadRepository>();
            //services.AddScoped<IPageWriteRepository, PageWriteRepository>();
            //services.AddScoped<ISettingReadRepository, SettingReadRepository>();
            //services.AddScoped<ISettingWriteRepository, SettingWriteRepository>();
            //services.AddScoped<ISubCategoryReadRepository, SubCategoryReadRepository>();
            //services.AddScoped<ISubCategoryWriteRepository, SubCategoryWriteRepository>();
            //services.AddScoped<ISubCategoryAdvertReadRepository, SubCategoryAdvertReadRepository>();
            //services.AddScoped<ISubCategoryAdvertWriteRepository, SubCategoryAdvertWriteRepository>();
            //services.AddScoped<IUserReadRepository, UserReadRepository>();
            //services.AddScoped<IUserWriteRepository, UserWriteRepository>();


        }


    }
}