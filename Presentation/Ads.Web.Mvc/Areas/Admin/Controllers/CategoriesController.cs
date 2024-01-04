using Ads.Application.DTOs.Category;
using Ads.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
    [Area("Admin")]
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
            var model = await _service.GetAllCategories();
            return View(model);
        }

        // GET: CategoriesController/Detail
        public async Task<IActionResult> Details(int id, string categoryName)
        {
            var subCategories = await _service.GetSubCategoriesByCategoryId(id);
            ViewData["CategoryName"] = categoryName;

            return View(subCategories);
        }


        // GET: CategoriesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CategoryDto categoryDto)
        {
            try
            {
                await _service.CreateAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
                return View(categoryDto);
            }
        }



        // GET: CategoriesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDto = await _service.GetCategoryByIdAsync(id);
            if (categoryDto == null)
            {
                return NotFound();
            }
            return View(categoryDto);
        }


        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto categoryDto)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                await _service.UpdateAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.CategoryExists(categoryDto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //}
            return View(categoryDto);
        }


        // GET: CategoriesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDto = await _service.GetCategoryByIdAsync(id);
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
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}