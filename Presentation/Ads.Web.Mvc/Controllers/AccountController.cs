using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Ads.Web.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


        //Register
        public IActionResult Register()
        {
            return View();
        }

        //Register
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
                    //user.UserImagePath = "user.jpg";
                    user.SettingId = 1;

                    // Kullanıcının şifresini hashleme
                    var passwordHasher = new PasswordHasher<User>();
                    user.Password = passwordHasher.HashPassword(user, user.Password);

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



        //Details
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



        //UserUpdate
        [RequestFormLimits(MultipartBodyLengthLimit = 10485760)]
        [RequestSizeLimit(10485760)] // 10 MB
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> UserUpdateAsync(User user, IFormFile UserImagePath)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(uguid))

                {
                    var updatedUser = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);
                    if (updatedUser != null)
                    {
                        updatedUser.Username = user.Username;
                        updatedUser.FirstName = user.FirstName;
                        updatedUser.LastName = user.LastName;
                        updatedUser.Phone = user.Phone;
                        updatedUser.Address = user.Address;
                        updatedUser.IsActive = user.IsActive;
                        updatedUser.CreatedDate = user.CreatedDate;

                        if (UserImagePath != null && UserImagePath.Length > 1)
                        {

                            user.UserImagePath = await FileHelper.FileLoaderAsync(UserImagePath, "/Img/UserImages/");

                            //var imagePath = "/Img/UserImages/" + Guid.NewGuid() + Path.GetExtension(UserImagePath.FileName);
                            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
                            //using (var stream = new FileStream(filePath, FileMode.Create))
                            //{
                            //    await UserImagePath.CopyToAsync(stream);
                            //}

                            updatedUser.UserImagePath = user.UserImagePath;
                        }
                        _service.Update(updatedUser);
                        await _service.SaveAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hata Oluştu: " + ex.Message);
            }
            return RedirectToAction("Details", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        //Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(CustomerLoginViewModel customerLoginViewModel)
        {
            try
            {
                var account = await _service.GetAsync(k => k.Email == customerLoginViewModel.Email && k.IsActive == true);

                if (account != null)
                {
                    var passwordHasher = new PasswordHasher<User>();

                    if (passwordHasher.VerifyHashedPassword(account, account.Password, customerLoginViewModel.Password) == PasswordVerificationResult.Success)
                    {
                        var role = _serviceRole.Get(r => r.Id == account.RoleId);
                        var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, account.FirstName),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.UserData, account.UserGuid.ToString())
                };

                        if (role != null)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Name));
                        }

                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(principal);

                        if (role?.Name == "Admin")
                        {
                            return Redirect("/Admin");
                        }

                        return Redirect("/Account/Details");
                    }
                }

                ModelState.AddModelError("", "Giriş Başarısız!");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            return View();
        }


        //ChangePassword
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> ChangePasswordAsync(string newPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(uguid))
                {
                    var user = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);

                    if (user != null)
                    {
                        var passwordHasher = new PasswordHasher<User>();
                        user.Password = passwordHasher.HashPassword(user, newPassword);

                        _service.Update(user);
                        await _service.SaveAsync();

                        // Kullanıcıya bilgilendirme mesajı gönder
                        TempData["SuccessMessage"] = "Your email has been successfully changed. Please login with your new email.";

                        // Kullanıcının oturumunu sonlandır
                        await HttpContext.SignOutAsync();

                        // Yönlendirme yaparak giriş sayfasına gönder
                        return RedirectToAction("Login");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            return RedirectToAction("Details");
        }


        //ChangeEmail
        [HttpPost]
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> ChangeEmailAsync(string newEmail)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(uguid))
                {
                    var user = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);

                    if (user != null)
                    {
                        // Eğer yeni e-posta adresi geçerliyse
                        if (!string.IsNullOrEmpty(newEmail))
                        {
                            user.Email = newEmail;

                            _service.Update(user);
                            await _service.SaveAsync();

                            // Kullanıcıya bilgilendirme mesajı gönder
                            TempData["SuccessMessage"] = "Your email has been successfully changed. Please login with your new email.";

                            // Kullanıcının oturumunu sonlandır
                            await HttpContext.SignOutAsync();

                            // Yönlendirme yaparak giriş sayfasına gönder
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You must enter a valid email address.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred!");
            }

            return RedirectToAction("Details");
        }

        [Authorize(Policy = "CustomerPolicy")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

    }
}
