using Ads.Application.DTOs.Category;
using Ads.Application.Services;
using Ads.Persistence;
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
      try
      {
        LoggerHelper.LogInformation("Detay işlemi başlatıldı. {CategoryId}", id);
        var subCategories = await _service.GetSubCategoriesByCategoryId(id);
        ViewData["CategoryName"] = categoryName;
        LoggerHelper.LogInformation("Detay işlemi başarıyla tamamlandı.  {CategoryId}", id);
        return View(subCategories);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Detay işlemi sırasında hata oluştu.  {CategoryId}", id);
        throw;
      }
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
        LoggerHelper.LogInformation("Oluşturma işlemi başlatıldı");
        await _service.CreateAsync(categoryDto);
        LoggerHelper.LogInformation("Oluşturma işlemi başarıyla tamamlandı");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Oluşturma işlemi sırasında hata oluştu");
        ModelState.AddModelError("", "Hata Oluştu!");
        return View(categoryDto);
      }
    }

    // GET: CategoriesController/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Düzenleme işlemi başlatıldı.  {CategoryId}", id);
        var categoryDto = await _service.GetCategoryByIdAsync(id);
        if (categoryDto == null)
        {
          LoggerHelper.LogInformation("Düzenleme işlemi sırasında kategori bulunamadı.  {CategoryId}", id);
          return NotFound();
        }
        return View(categoryDto);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Düzenleme işlemi sırasında hata oluştu.  {CategoryId}", id);
        throw;
      }
    }

    // POST: CategoriesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryDto categoryDto)
    {
      try
      {
        LoggerHelper.LogInformation("Düzenleme işlemi başlatıldı.  {CategoryId}", categoryDto.Id);
        await _service.UpdateAsync(categoryDto);
        LoggerHelper.LogInformation("Düzenleme işlemi başarıyla tamamlandı. {CategoryId}", categoryDto.Id);
        return RedirectToAction(nameof(Index));
      }
      catch (DbUpdateConcurrencyException)
      {
        LoggerHelper.LogWarning("Düzenleme işlemi sırasında kategori bulunamadı.  {CategoryId}", categoryDto.Id);
        return NotFound();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Düzenleme işlemi sırasında beklenmeyen bir hata oluştu.  {CategoryId}", categoryDto.Id);
        throw;
      }
    }

    // GET: CategoriesController/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Silme işlemi başlatıldı.  {CategoryId}", id);
        var categoryDto = await _service.GetCategoryByIdAsync(id);
        if (categoryDto == null)
        {
          LoggerHelper.LogWarning("Silme işlemi sırasında kategori bulunamadı.  {CategoryId}", id);
          return NotFound();
        }
        return View(categoryDto);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Silme işlemi sırasında hata oluştu.{CategoryId}", id);
        throw;
      }
    }

    // POST: CategoriesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Silme işlemi başlatıldı.  {CategoryId}", id);
        await _service.DeleteAsync(id);
        LoggerHelper.LogInformation("Silme işlemi başarıyla tamamlandı. {CategoryId}", id);
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Silme işlemi sırasında hata oluştu.  {CategoryId}", id);
        throw;
      }
    }
  }
}