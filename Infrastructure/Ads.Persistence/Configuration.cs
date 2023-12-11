using Microsoft.Extensions.Configuration;

namespace Ads.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Ads.Web.Mvc"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DBConStr");
            }
        }
    }
}
