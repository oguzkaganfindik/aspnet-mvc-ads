using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Models.Settings
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class SettingsController : Controller
    {
        private readonly ISettingService _service;
        private readonly IService<User> _serviceUser;

        public SettingsController(ISettingService service, IService<User> serviceUser)
        {
            _service = service;
            _serviceUser = serviceUser;
        }

        // GET: SettingsController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomSettingList();
            return View(model);
        }

        // GET: SettingsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: SettingsController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View();
        }

        // POST: SettingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Setting setting)
        {
            try
            {
                await _service.AddAsync(setting);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View(setting);
        }

        // GET: SettingsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View(model);
        }

        // POST: SettingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Setting setting)
        {
            try
            {
                _service.Update(setting);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View(setting);
        }

        // GET: SettingsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SettingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, Setting setting)
        {
            try
            {
                _service.Delete(setting);
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
