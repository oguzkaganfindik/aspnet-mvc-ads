using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userDto = await _service.GetUserByIdAsync(id);
            if (userDto == null)
                return NotFound();

            return View(userDto);
        }


        // GET: UsersController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto userDto, IFormFile? UserImagePath)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateUserAsync(userDto, UserImagePath);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(IndexAsync));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(userDto);
        }


        //// GET: UsersController/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var userDto = await _service.GetUserByIdAsync(id);
        //    if (userDto == null)
        //    {
        //        return NotFound();
        //    }

        //    // Rol ve SettingId seçimlerini ViewBag ile taşıyabilirsiniz, eğer formda seçim yapılacaksa.
        //    ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name", userDto.Role?.Name);
        //    ViewBag.SettingId = new SelectList(await _context.Settings.ToListAsync(), "Id", "Name", userDto.SettingId);

        //    return View(userDto);
        //}

        //// POST: UsersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, UserDto userDto, IFormFile? userImageFile)
        //{
        //    if (id != userDto.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _service.EditUserAsync(userDto, userImageFile);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    // Rol ve SettingId seçimlerini ViewBag ile taşıyabilirsiniz, eğer formda seçim yapılacaksa.
        //    ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name", userDto.Role?.Name);
        //    ViewBag.SettingId = new SelectList(await _context.Settings.ToListAsync(), "Id", "Name", userDto.SettingId);

        //    return View(userDto);
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
