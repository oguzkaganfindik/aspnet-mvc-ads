using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Ads.Web.Mvc.Models;
using Bogus.DataSets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Ads.Web.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;
        private readonly IService<Role> _serviceRole;
        private readonly IService<Setting> _serviceSetting;

        public AccountController(IUserService service, IService<Role> serviceRol, IService<Setting> serviceSetting)
        {
            _service = service;
            _serviceRole = serviceRol;
            _serviceSetting = serviceSetting;
        }

        [Authorize(Policy = "CustomerPolicy")]
        public IActionResult Details()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
            if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
            {
                var user = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.IsActive = true;
                    user.CreatedDate = DateTime.Now;
                    user.RoleId = 3;
                    user.UserImagePath = "user.jpg";
                    user.SettingId = 1;
                    await _service.AddAsync(user);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Details));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Hata Oluştu: {ex.Message}");
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu.");
                }
            }
            return View(user);
        }



        [HttpPost]
        public IActionResult UserUpdate(User users)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
                {
                    var user = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);
                    if (user != null)
                    {
                        user.FirstName = users.FirstName;
                        user.LastName = users.LastName;
                        user.Email = users.Email;
                        user.Password = users.Password;
                        user.IsActive = users.IsActive;
                        user.UserGuid = users.UserGuid;
                        user.Phone = users.Phone;
                        _service.Update(user);
                        _service.Save();
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Hata Oluştu!");
            }

            return RedirectToAction("Details");
        }

       


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(CustomerLoginViewModel customerLoginViewModel)
        {
            try
            {
                var account = await _service.GetAsync(k => k.Email == customerLoginViewModel.Email && k.Password == customerLoginViewModel.Password && k.IsActive == true);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    var role = _serviceRole.Get(r => r.Id == account.RoleId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.FirstName),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.UserData, account.UserGuid.ToString())
                    };
                    if (role is not null)
                    {
                        //claims.Add(new Claim("Role", rol.Adi));
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    if (role.Name == "Admin")
                    {
                        return Redirect("/Admin");
                    }
                    return Redirect("/Account/Details");

                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

    }
}
