using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Ads.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;

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
    public async Task<IActionResult> Details(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Details işlemi başlatıldı - Id: {Id}", id);
        // Detay işlemleri burada gerçekleştirilebilir.
        LoggerHelper.LogInformation("Details işlemi başarıyla tamamlandı - Id: {Id}", id);
        return View();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Details işlemi sırasında hata oluştu - Id: {Id}", id);
        throw;
      }
    }

    // GET: SubCategoriesController/Create
    public async Task<IActionResult> CreateAsync()
    {
      try
      {
        LoggerHelper.LogInformation("Create işlemi başlatıldı");
        ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
        LoggerHelper.LogInformation("Create işlemi başarıyla tamamlandı");
        return View();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Create işlemi sırasında hata oluştu");
        throw;
      }
    }

    // POST: SubCategoriesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(SubCategory subCategory)
    {
      try
      {
        LoggerHelper.LogInformation("Create işlemi başlatıldı");
        await _service.AddAsync(subCategory);
        await _service.SaveAsync();
        LoggerHelper.LogInformation("Create işlemi başarıyla tamamlandı");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Create işlemi sırasında hata oluştu");
        ModelState.AddModelError("", "Hata Oluştu!");
        return View(subCategory);
      }
    }

    // GET: SubCategoriesController/Edit/5
    public async Task<IActionResult> EditAsync(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Edit işlemi başlatıldı - Id: {Id}", id);
        ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
        var model = await _service.FindAsync(id);
        LoggerHelper.LogInformation("Edit işlemi başarıyla tamamlandı - Id: {Id}", id);
        return View(model);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Edit işlemi sırasında hata oluştu - Id: {Id}", id);
        throw;
      }
    }

    // POST: SubCategoriesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, SubCategory subCategory)
    {
      try
      {
        LoggerHelper.LogInformation("Edit işlemi başlatıldı - Id: {Id}", id);
        _service.Update(subCategory);
        await _service.SaveAsync();
        LoggerHelper.LogInformation("Edit işlemi başarıyla tamamlandı - Id: {Id}", id);
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Edit işlemi sırasında hata oluştu - Id: {Id}", id);
        ModelState.AddModelError("", "Hata Oluştu!");
        ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
        return View(subCategory);
      }
    }

    // GET: SubCategoriesController/Delete/5
    public async Task<IActionResult> DeleteAsync(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Delete işlemi başlatıldı - Id: {Id}", id);
        var model = await _service.FindAsync(id);
        LoggerHelper.LogInformation("Delete işlemi başarıyla tamamlandı - Id: {Id}", id);
        return View(model);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Delete işlemi sırasında hata oluştu - Id: {Id}", id);
        throw;
      }
    }

    // POST: SubCategoriesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(int id, SubCategory subCategory)
    {
      try
      {
        LoggerHelper.LogInformation("Delete işlemi başlatıldı - Id: {Id}", id);
        _service.Delete(subCategory);
        await _service.SaveAsync();
        LoggerHelper.LogInformation("Delete işlemi başarıyla tamamlandı - Id: {Id}", id);
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Delete işlemi sırasında hata oluştu - Id: {Id}", id);
        return View();
      }
    }
  }
}
