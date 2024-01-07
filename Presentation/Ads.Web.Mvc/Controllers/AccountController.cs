using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Ads.Application.ViewModels;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Ads.Persistence.Services;
using Ads.Web.Mvc.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;


namespace Ads.Web.Mvc.Controllers
{
    public class AccountController : Controller
    {


        private readonly IService<Setting> _serviceSetting;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IUserService _service;


        public AccountController(IService<Setting> serviceSetting, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IMapper mapper, RoleManager<AppRole> roleManager, IUserService service, IHttpContextAccessor httpContextAccessor)
        {
            _serviceSetting = serviceSetting;

            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
            _roleManager = roleManager;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }



        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser appuser = _mapper.Map<AppUser>(request);

            appuser.SettingId = 1;

            var identityResult = await _userManager.CreateAsync(appuser, request.Password);
            if (!identityResult.Succeeded)
            {
                ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
                return View();

            }

            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == "User");

            await _userManager.AddToRoleAsync(appuser, role.Name);

            var exchangeExpireClaim = new Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString());


            var claimResult = await _userManager.AddClaimAsync(appuser!, exchangeExpireClaim);
            if (!claimResult.Succeeded)
            {
                ModelState.AddModelErrorList(claimResult.Errors.Select(x => x.Description).ToList());
                return View();

            }
            TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarı ile gerçekleşmiştir.";
            return RedirectToAction(nameof(Login));

        }



        //Register//signup


        //Details
        //[Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> Details()
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            var userEditDto = _mapper.Map<UserEditDto>(user);

            EditUserViewModel editUserViewModel = new()
            {
                UserEditDto = userEditDto,

            };

            return View(editUserViewModel);
            

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> UserUpdateAsync(EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Eğer model geçerli değilse, hataları göster ve düzenleme sayfasına geri dön
                return View(editUserViewModel);
            }

            var result = await _service.UpdateUserAsync(editUserViewModel.UserEditDto , editUserViewModel.File);

            if (result.Succeeded)
            {
                // Başarılı güncelleme durumunda, kullanıcıyı başka bir sayfaya yönlendir
                return RedirectToAction("Index" , "Home");
            }
            else
            {
                // Başarısız güncelleme durumunda, hataları göster ve düzenleme sayfasına geri dön
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                var allRoles = await _roleManager.Roles.ToListAsync(); // RolManager ile tüm rolleri çek
                ViewBag.Roles = new SelectList(allRoles, "Name", "Name", editUserViewModel.UserEditDto.Roles.FirstOrDefault());
                ModelState.Clear();
                return View("Edit", editUserViewModel);
            }

        }



        public IActionResult Login()
        {
            return View();
        }

        //Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto customerLoginViewModel, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home");

            //Bunları niye silmediniz :)


            if (string.IsNullOrEmpty(customerLoginViewModel.Email))
            {
                ModelState.AddModelError(string.Empty, "Email adresi boş olamaz.");
                return View(customerLoginViewModel);
            }

            var hasUser = await _userManager.FindByEmailAsync(customerLoginViewModel.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış.");
                return View();
            }


            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, customerLoginViewModel.Password, customerLoginViewModel.RememberMe, true);


            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giriş yapamazsınız." });
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Email veya şifre yanlış.", $"Başarısız giriş sayısı: {await _userManager.GetAccessFailedCountAsync(hasUser)}" });
                return View(customerLoginViewModel);
            }

            //Response.Cookies.Append("UserId", hasUser.Id.ToString());
            //if (hasUser.BirthDate.HasValue)
            //{
            //    await _signInManager.SignInWithClaimsAsync(hasUser, customerLoginViewModel.RememberMe, new[] { new Claim("birthdate", hasUser.BirthDate.Value.ToString()) });
            //}

            return Redirect(returnUrl!);
        }


        //ChangePassword
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> ChangePasswordAsync(string newPassword)
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> ChangeEmailAsync(string newEmail)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User); // Mevcut kullanıcıyı al

                if (user != null && !string.IsNullOrEmpty(newEmail))
                {
                    var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail); // E-posta değişikliği için token oluştur

                    var result = await _userManager.ChangeEmailAsync(user, newEmail, emailToken); // E-posta değişikliğini gerçekleştir

                    if (result.Succeeded)
                    {
                        // Kullanıcıya bilgilendirme mesajı gönder
                        TempData["SuccessMessage"] = "Your email has been successfully changed. Please check your new email for confirmation.";

                        // Eğer istenirse kullanıcının oturumunu sonlandırabilirsiniz
                        // await HttpContext.SignOutAsync();

                        // Yönlendirme yaparak ayrıntılar sayfasına gönder
                        return RedirectToAction("Details", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You must enter a valid email address.");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred!");
            }

            return RedirectToAction("Details", "Account");
        }



        //ChangeEmail
        //[HttpPost]
        //[Authorize(Policy = "CustomerPolicy")]
        //public async Task<IActionResult> ChangeEmailAsync(string newEmail)
        //{
        //    //try
        //    //{
        //    //    var email = User.FindFirst(ClaimTypes.Email)?.Value;
        //    //    var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;

        //    //    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(uguid))
        //    //    {
        //    //        var user = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);

        //    //        if (user != null)
        //    //        {
        //    //            // Eğer yeni e-posta adresi geçerliyse
        //    //            if (!string.IsNullOrEmpty(newEmail))
        //    //            {
        //    //                user.Email = newEmail;

        //    //                _service.Update(user);
        //    //                await _service.SaveAsync();

        //    //                // Kullanıcıya bilgilendirme mesajı gönder
        //    //                TempData["SuccessMessage"] = "Your email has been successfully changed. Please login with your new email.";

        //    //                // Kullanıcının oturumunu sonlandır
        //    //                await HttpContext.SignOutAsync();

        //    //                // Yönlendirme yaparak giriş sayfasına gönder
        //    //                return RedirectToAction("Login");
        //    //            }
        //    //            else
        //    //            {
        //    //                ModelState.AddModelError("", "You must enter a valid email address.");
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    ModelState.AddModelError("", "An error occurred!");
        //    //}

        //    //
        //    //
        //    //
        //    return RedirectToAction("Details");
        //}



        //[Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "/");
        }
        //[Authorize(Policy = "CustomerPolicy")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto request)
        {
            AppUser appUser = _mapper.Map<AppUser>(request);
            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Bu email adresine sahip kullanıcı bulunamamıştır.");
                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword", "Account",
                new { userId = hasUser.Id, Token = passwordResetToken }, HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmail(passwordResetLink, hasUser.Email);

            TempData["success"] = "Şifre yenileme linki, e-posta adresinize gönderilmiştir.";
            return RedirectToAction(nameof(ForgotPassword));

        }
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {

            string userId = TempData["userId"].ToString();
            string token = TempData["token"].ToString();
            if (userId == null | token == null)
            {
                throw new Exception("Bir hata meydana geldi");
            }
            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);
            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanıcı bulunamamıştır");
                return View();

            }
            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla yenilenmiştir";
                await _userManager.UpdateSecurityStampAsync(hasUser);
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
            }
            return View();
        }
    }
}
