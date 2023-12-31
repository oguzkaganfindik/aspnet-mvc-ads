using Ads.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

public class DatabaseInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();

            // Diğer başlatma işlemleri buraya eklenebilir

            bool isDatabaseCreated = context.Database.EnsureCreated();
            if (isDatabaseCreated)
            {
                //DbSeeder.SeedData(context);
            }
        }
    }
}
