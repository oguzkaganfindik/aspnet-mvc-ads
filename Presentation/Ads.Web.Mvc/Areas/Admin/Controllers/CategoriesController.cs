using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // GET: CategoriesController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomCategoryList();
            return View(model);
        }

        // GET: CategoriesController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Category category, IFormFile? CategoryIconPath)
        {
            try
            {
                category.CategoryIconPath = await FileHelper.FileLoaderAsync(CategoryIconPath, "/Img/CategoryIconImages/");
                await _service.AddAsync(category);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            
                return View(category);
        }

        // GET: CategoriesController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Category category, IFormFile? CategoryIconPath)
        {
            try
            {
                if (CategoryIconPath is not null)
                {
                    category.CategoryIconPath = await FileHelper.FileLoaderAsync(CategoryIconPath, "/Img/CategoryIconImages/");
                }
                _service.Update(category);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            
                return View(category);
        }

        // GET: CategoriesController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, Category category)
        {
            try
            {
                _service.Delete(category);
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
