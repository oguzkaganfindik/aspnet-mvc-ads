using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class DatabaseInitializer
{
    public static void Initialize(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            bool isDatabaseCreated = context.Database.EnsureCreated();
            if (isDatabaseCreated)
            {
                DbSeeder.SeedData(context);
            }
        }
    }
}
