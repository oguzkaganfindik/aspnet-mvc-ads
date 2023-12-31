using Ads.Application.FluentValidation;
using Ads.Application.Mapping;
using Ads.Application.OptionsModel;
using Ads.Application.Services;
using Ads.Persistence;
using Ads.Web.Mvc.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{

    options.ValidationInterval = TimeSpan.FromMinutes(30);
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddPersistenceServices();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddValidatorsFromAssemblyContaining<AdvertDtoValidator>();

builder.Services.AddControllers();

builder.Services.ConfigureApplicationCookie(opt =>
{
var cookieBuilder = new CookieBuilder();
cookieBuilder.Name = "NewsAppCookie";
    opt.LoginPath = new PathString("/account/login");
    opt.LogoutPath = new PathString("/account/Logout");
    opt.AccessDeniedPath = new PathString("/AccessDenied");
    opt.Cookie.Name = "admin";
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(15);
    opt.SlidingExpiration = true; //cookie süresini müdafaa ediyoruz
});


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
//{
//    x.LoginPath = "/Account/Login";
//    x.AccessDeniedPath = "/AccessDenied";
//    x.LogoutPath = "/Account/Logout";
//    x.Cookie.Name = "Admin";
//    x.Cookie.MaxAge = TimeSpan.FromDays(7);
//    x.Cookie.IsEssential = true;
//});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Moderator"));
    x.AddPolicy("CustomerPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Moderator", "User"));
});

builder.Services.AddIdentityWithExt();
var app = builder.Build();


DatabaseInitializer.Initialize(app.Services);


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
