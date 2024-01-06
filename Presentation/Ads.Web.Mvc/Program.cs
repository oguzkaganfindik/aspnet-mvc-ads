using Ads.Application.FluentValidation;
using Ads.Application.Mapping;
using Ads.Persistence;

using Ads.Web.Mvc.Extensions;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FluentValidation;
using Ads.Application.OptionsModel;
using Ads.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{

    options.ValidationInterval = TimeSpan.FromMinutes(30);
});
builder.Services.AddIdentityWithExt();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddScoped<IMemberService, MemberService>();
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


builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
    x.AddPolicy("Moderator", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Customer"));
});

WebApplication? app = builder.Build();

app.Initialize();

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
