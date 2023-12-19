using Microsoft.Extensions.Configuration;

namespace Ads.Persistence
{
    static class Configuration
    {
        static IConfiguration ConfigurationInstance { get; }

        static Configuration()
        {
            ConfigurationInstance = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Ads.Web.Mvc"))
                .AddJsonFile("appsettings.json")
                .Build();
        }

        static public string ConnectionString
        {
            get
            {
                return ConfigurationInstance.GetConnectionString("DBConStr");
            }
        }
    }
}
