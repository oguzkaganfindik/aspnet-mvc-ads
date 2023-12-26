using Ads.Application.FluentValidation;
using Ads.Application.Mapping;
using Ads.Persistence;
using Ads.Persistence.Contexts;
using Ads.Persistence.DataContext;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddPersistenceServices();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<AdvertDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<AdvertCommentDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<AdvertImageDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<AdvertRatingDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<CategoryDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<CategoryAdvertDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<PageDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<RoleDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<SettingDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<SubCategoryDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<SubCategoryAdvertDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<ForgotPasswordDtoValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>();

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Account/Login";
    x.AccessDeniedPath = "/AccessDenied";
    x.LogoutPath = "/Account/Logout";
    x.Cookie.Name = "Admin";
    x.Cookie.MaxAge = TimeSpan.FromDays(7);
    x.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
    x.AddPolicy("CustomerPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Customer"));
});


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbInitializer = scope.ServiceProvider.GetRequiredService<Ads.Persistence.Initializer.IDbInitializer>();
//    dbInitializer.Initialize();
//}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    bool isDatabaseCreated = context.Database.EnsureCreated();
    if (isDatabaseCreated)
    {
        DbSeeder.SeedData(context);
    }
}

    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
