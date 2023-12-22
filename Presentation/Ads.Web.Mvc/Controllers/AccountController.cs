using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Ads.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //[Authorize(Policy = "CustomerPolicy")]
        public IActionResult Index()
        {
            //var email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
            //if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
            //{
            //    var user = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);
            //    if (user != null)
            //    {
            //        return View(user);
            //    }

            //}
            //return NotFound();
            return View();
        }

        [HttpPost]
        public IActionResult UserUpdate(User user)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
                {
                    var users = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);
                    if (user != null)
                    {
                        user.FirstName = users.FirstName;
                        user.LastName = users.LastName;
                        user.Email = users.Email;
                        user.Password = users.Password;
                        user.IsActive = users.IsActive;
                        user.UserGuid = users.UserGuid;
                        //user.CreatedDate = users.CreatedDate;
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

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RegisterAsync()
        {
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Theme");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
                    ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Theme");
                    //var role = await _serviceRole.GetAsync(r => r.Name == "Customer");
                    //if (role == null)
                    //{
                    //    ModelState.AddModelError("", "Kayıt Başarısız!");
                    //    return View();
                    //}
                    //user.RoleId = role.Id;
                    //user.IsActive = true;
                    await _service.AddAsync(user);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Theme");
            return View(user);
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
                    return Redirect("/Account");

                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
