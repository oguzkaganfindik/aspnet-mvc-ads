using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Ads.Web.Mvc.Extensions;
using AutoMapper;
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


        private readonly IService<Setting> _serviceSetting;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;


        public AccountController(IService<Setting> serviceSetting, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IMapper mapper, RoleManager<AppRole> roleManager)
        {
            _serviceSetting = serviceSetting;

            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
            _roleManager = roleManager;
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
            return RedirectToAction(nameof(AccountController.Register));

        }



        //Register//signup


        //Details
        //[Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> DetailsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                return View(user);
            }

            return NotFound();


        }



        ////UserUpdate
        //[RequestFormLimits(MultipartBodyLengthLimit = 10485760)]
        //[RequestSizeLimit(10485760)] // 10 MB
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = "CustomerPolicy")]
        //public async Task<IActionResult> UserUpdateAsync(AppUser user, IFormFile UserImagePath)
        //{
        //    try
        //    {
        //        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        //        var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
        //        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(uguid))

        //        {
        //            var updatedUser = _service.Get(k => k.Email == email && k.UserGuid.ToString() == uguid);
        //            if (updatedUser != null)
        //            {
        //                updatedUser.Username = user.Username;
        //                updatedUser.FirstName = user.FirstName;
        //                updatedUser.LastName = user.LastName;
        //                updatedUser.Phone = user.Phone;
        //                updatedUser.Address = user.Address;
        //                updatedUser.IsActive = user.IsActive;
        //                updatedUser.CreatedDate = user.CreatedDate;

        //                if (UserImagePath != null && UserImagePath.Length > 1)
        //                {

        //                    user.UserImagePath = await FileHelper.FileLoaderAsync(UserImagePath, "/Img/UserImages/");

        //                    //var imagePath = "/Img/UserImages/" + Guid.NewGuid() + Path.GetExtension(UserImagePath.FileName);
        //                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
        //                    //using (var stream = new FileStream(filePath, FileMode.Create))
        //                    //{
        //                    //    await UserImagePath.CopyToAsync(stream);
        //                    //}

        //                    updatedUser.UserImagePath = user.UserImagePath;
        //                }
        //                _service.Update(updatedUser);
        //                await _service.SaveAsync();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Hata Oluştu: " + ex.Message);
        //    }
        //    return RedirectToAction("Details", "Account");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> UserUpdateAsync(AppUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User); // Kullanıcıyı al

                    if (user != null)
                    {
                        user.UserName = model.UserName;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.PhoneNumber = model.Phone;
                        user.Address = model.Address;
                        user.IsActive = model.IsActive;
                        user.CreatedDate = model.CreatedDate;

                        if (model.UserImagePath != null && model.UserImagePath.Length > 0)
                        {
                            user.UserImagePath = await FileHelper.FileLoaderAsync(model.UserImagePath, "/Img/UserImages/");
                            // Kullanıcı resmini güncelleme
                        }

                        var result = await _userManager.UpdateAsync(user); // Kullanıcıyı güncelle

                        if (result.Succeeded)
                        {
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
        public async Task<IActionResult> LoginAsync(LoginDto customerLoginViewModel, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

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
