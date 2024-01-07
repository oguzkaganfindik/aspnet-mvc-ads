using Ads.Application.DTOs.Advert;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Controllers
{
    public class AdvertController : Controller
  {
        private readonly IAdvertService _advertService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryService _serviceCategory;
        private readonly ISubCategoryService _serviceSubCategory;

        public AdvertController(IAdvertService advertService, UserManager<AppUser> userManager, ICategoryService serviceCategory, ISubCategoryService serviceSubCategory)
        {
            _advertService = advertService;
            _userManager = userManager;
            _serviceCategory = serviceCategory;
            _serviceSubCategory = serviceSubCategory;
        }

        public async Task<IActionResult> Search(string query, int page = 1, int pageSize = 5)
        {
            var model = await _advertService.Search(query, page, pageSize);
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var adverts = await _advertService.GetAllAdvertsAsync();
            return View(adverts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var advert = await _advertService.GetAdvertDetailsAsync(id);
            if (advert == null)
            {
                return NotFound();
            }

            return View(advert);
        }

        // GET: Adverts/Create
        public async Task<IActionResult> Create()
        {
            if (_userManager != null)
            {
                var users = await _userManager.Users.ToListAsync();
                ViewBag.UserId = new SelectList(users, "Id", "UserName");
                ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
                ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
            }
            return View();
        }

        // POST: Adverts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _advertService.AddAdvertAsync(advertDto, selectedCategoryIds, selectedSubCategoryIds);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Hata yönetimi: Loglama, kullanıcıya hata mesajı gösterme vs.
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }

            // Hata durumunda formu tekrar doldurmak için gerekli verileri ViewBag ile gönder
            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View(advertDto);
        }


        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCategories = await _serviceSubCategory.GetAllAsync(sc => sc.CategoryId == categoryId);
            var result = subCategories.Select(sc => new { value = sc.Id, text = sc.Name }).ToList();

            return Json(result);
        }


        // GET: CategoriesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDto = await _advertService.GetAdvertDetailsAsync(id);
            if (categoryDto == null)
            {
                return NotFound();
            }

            return View(categoryDto);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _advertService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}