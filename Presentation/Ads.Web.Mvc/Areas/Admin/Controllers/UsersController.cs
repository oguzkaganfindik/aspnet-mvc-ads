using Ads.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: UsersController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllUsersAsync();
            return View(model);
        }

        //// GET: UsersController/Details/5
        //public IActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: UsersController/Create
        //public async Task<IActionResult> CreateAsync()
        //{
        //    ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
        //    ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Key");
        //    return View();
        //}

        //// POST: UsersController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateAsync(AppUser user, IFormFile? UserImagePath)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            user.UserImagePath = await FileHelper.FileLoaderAsync(UserImagePath, "/Img/UserImages/");
        //            await _service.AddAsync(user);
        //            await _service.SaveAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            ModelState.AddModelError("", "Hata Oluştu!");
        //        }
        //    }
        //    ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
        //    ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Key");
        //    return View(user);
        //}

        //// GET: UsersController/Edit/5
        //public async Task<IActionResult> EditAsync(int id)
        //{
        //    var model = await _service.FindAsync(id);
        //    ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
        //    ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Key");
        //    return View(model);
        //}

        //// POST: UsersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAsync(int id, AppUser user, IFormFile? UserImagePath)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (UserImagePath is not null)
        //            {
        //                user.UserImagePath = await FileHelper.FileLoaderAsync(UserImagePath, "/Img/UserImages/");
        //            }

        //            _service.Update(user);
        //            await _service.SaveAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            ModelState.AddModelError("", "Hata Oluştu!");
        //        }
        //    }
        //    ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
        //    ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Key");
        //    return View(user);
        //}

        //// GET: UsersController/Delete/5
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    var model = await _service.FindAsync(id);
        //    return View(model);
        //}

        //// POST: UsersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteAsync(int id, AppUser user)
        //{
        //    try
        //    {
        //        _service.Delete(user);
        //        await _service.SaveAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
