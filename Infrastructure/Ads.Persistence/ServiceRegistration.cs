using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
//using Ads.Persistence.Initializer;

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
            services.AddScoped<IRoleService, RoleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IAdvertCommentService, AdvertCommentService>();
            services.AddTransient<IAdvertImageService, AdvertImageService>();
            services.AddTransient<IAdvertRatingService, AdvertRatingService>();
            services.AddScoped<INavbarService, NavbarService>();
            services.AddScoped<IPageService, PageService>();



      ConfigureLogger();
    }


    private static void ConfigureLogger()
    {
      var configuration = new ConfigurationBuilder()
         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .Build();

      var loggerConfiguration = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
          .WriteTo.MSSqlServer(
              connectionString: configuration.GetConnectionString("DBConStr"),
              tableName: "logs",
              autoCreateSqlTable: true,
              columnOptions: new ColumnOptions
              {
                AdditionalColumns = new Collection<SqlColumn>
                  {
                        new SqlColumn { ColumnName = "logMessage", DataType = SqlDbType.NVarChar, DataLength = 4000 },
                        new SqlColumn { ColumnName = "message_template", DataType = SqlDbType.NVarChar, DataLength = 4000 },
                        new SqlColumn { ColumnName = "logLevel", DataType = SqlDbType.NVarChar, DataLength = 128 },
                        new SqlColumn { ColumnName = "time_stamp", DataType = SqlDbType.DateTimeOffset },
                        new SqlColumn { ColumnName = "logException", DataType = SqlDbType.NVarChar, DataLength = 4000 },
                        new SqlColumn { ColumnName = "user_name", DataType = SqlDbType.NVarChar, DataLength = 256 }
												// Diğer özel sütunları ekleyin
									}
              }
          );

      Log.Logger = loggerConfiguration.CreateLogger();
    }
  }
}