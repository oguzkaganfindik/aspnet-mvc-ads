using Ads.Application.DTOs.Page;
using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Ads.Application.ViewModels;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserService _service;
        private readonly AppDbContext _context;


        public UsersController(IUserService service, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext context)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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
            var userDto = await _service.GetUserByIdAsync<UserDto>(id);
            if (userDto == null)
                return NotFound();

            return View(userDto);
        }


        // GET: UsersController/Create
        public async Task<IActionResult> Create()
        {

            var allRoles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(allRoles, "Id", "Name");
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
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            var allRoles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(allRoles, "Name", "Name");
            return View(userDto);
        }


        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            UserEditDto? userEditDto = await _service.GetUserByIdAsync<UserEditDto>(id);
            if (userEditDto == null)
            {
                return NotFound();
            }

            var allRoles = await _roleManager.Roles.ToListAsync(); // RolManager ile tüm rolleri çek
            ViewBag.Roles = new SelectList(allRoles, "Id", "Name", userEditDto.Roles.FirstOrDefault());

            userEditDto.RoleId = allRoles.FirstOrDefault(x => x.Name == userEditDto.Roles.FirstOrDefault()).Id;

            EditUserViewModel editUserViewModel = new()
            {
                UserEditDto = userEditDto,
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Eğer model geçerli değilse, hataları göster ve düzenleme sayfasına geri dön
                return View(editUserViewModel);
            }

            var result = await _service.UpdateUserAsync(editUserViewModel.UserEditDto, editUserViewModel.File);

            if (result.Succeeded)
            {
                // Başarılı güncelleme durumunda, kullanıcıyı başka bir sayfaya yönlendir
                return RedirectToAction("Index");
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

                return View("Edit", editUserViewModel);

            }
        }



        //// POST: UsersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, UserDto userDto, IFormFile? userImageFile)
        //{
        //    if (id != userDto.Id)
        //    {
        //        return NotFound();
        //    }

        //if (ModelState.IsValid)
        //{
        //    var result = await _userManager.UpdateAsync(userDto, userImageFile);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //}

        // Rol ve SettingId seçimlerini ViewBag ile taşıyabilirsiniz, eğer formda seçim yapılacaksa.
        //    ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name", userDto.Role?.Name);
        //    ViewBag.SettingId = new SelectList(await _context.Settings.ToListAsync(), "Id", "Name", userDto.SettingId);

        //    return View(userDto);
        //}

        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {

                return RedirectToAction(nameof(Index));
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(user);
        }
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
