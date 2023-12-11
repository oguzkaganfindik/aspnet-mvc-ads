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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<IAdvertRepository, AdvertRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }


    }
}