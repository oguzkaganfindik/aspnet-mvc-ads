using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _service;
        private readonly IService<Role> _serviceRole;
        private readonly IService<Setting> _serviceSetting;

        public UsersController(IUserService service, IService<Role> serviceRole, IService<Setting> serviceSetting)
        {
            _service = service;
            _serviceRole = serviceRole;
            _serviceSetting = serviceSetting;
        }



        // GET: UsersController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomList(); 
            return View(model);
        }

        // GET: UsersController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Theme");
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(User user, IFormFile? UserImagePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.UserImagePath = await FileHelper.FileLoaderAsync(UserImagePath, "/Img/UserImages/");
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

        // GET: UsersController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            ViewBag.SettingId = new SelectList(await _serviceSetting.GetAllAsync(), "Id", "Theme");
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, User user, IFormFile? UserImagePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (UserImagePath is not null)
                    {
                        user.UserImagePath = await FileHelper.FileLoaderAsync(UserImagePath, "/Img/UserImages/");
                    }

                    _service.Update(user);
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

        // GET: UsersController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, User user)
        {
            try
            {
                _service.Delete(user);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
