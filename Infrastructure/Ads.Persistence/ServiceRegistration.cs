//using Ads.Application.Repositories;
//using Ads.Application.Services;
//using Ads.Domain.Entities.Concrete;
//using Ads.Persistence.Contexts;
//using Ads.Persistence.Repositories;
//using Ads.Persistence.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Ads.Persistence
//{
//    public static class ServiceRegistration
//    {
//        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConStr")));
//            //services.AddScoped<IAdvertRepository, AdvertRepository>();
//            //services.AddScoped<IUserRepository, UserRepository>();

//            services.AddTransient(typeof(IService<>), typeof(Service<>));
//            services.AddTransient<IAdvertService, AdvertService>();
//            services.AddTransient<IUserService, UserService>();
//        }


//    }
//}