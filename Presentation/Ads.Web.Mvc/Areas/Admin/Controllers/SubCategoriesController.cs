using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
    [Area("Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryService _service;
        private readonly IService<Category> _serviceCategory;

        public SubCategoriesController(ISubCategoryService service, IService<Category> serviceCategory)
        {
            _service = service;
            _serviceCategory = serviceCategory;
        }



        // GET: SubCategoriesController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomSubCategoryList();
            return View(model);
        }

        // GET: SubCategoriesController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: SubCategoriesController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: SubCategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(SubCategory subCategory)
        {
            try
            {
                await _service.AddAsync(subCategory);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View(subCategory);
        }

        // GET: SubCategoriesController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SubCategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, SubCategory subCategory)
        {
            try
            {
                _service.Update(subCategory);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            return View(subCategory);
        }

        // GET: SubCategoriesController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SubCategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, SubCategory subCategory)
        {
            try
            {
                _service.Delete(subCategory);
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
