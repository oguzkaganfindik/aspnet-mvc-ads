//using Microsoft.Extensions.Configuration;

//namespace Ads.Persistence
//{
//    static class Configuration
//    {
//        static public string ConnectionString
//        {
//            get
//            {
//                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
//                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Ads.Web.Mvc"))
//                    .AddJsonFile("appsettings.json");

//                IConfiguration configuration = configurationBuilder.Build();

//                return configuration.GetConnectionString("DBConStr");
//            }
//        }
//    }
//}
