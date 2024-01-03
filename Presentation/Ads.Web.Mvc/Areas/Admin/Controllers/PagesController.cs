using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Infrastructure.Services;
using Ads.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
  public class PagesController : Controller
  {
    private readonly IPageService _service;

    public PagesController(IPageService service)
    {
      _service = service;
    }

   

    // GET: PagesController
    public async Task<ActionResult> Index()
    {
      //

      List<PageDto> pageDtos = await _service.GetAllPagesWithSettingsAsync();
      return View(pageDtos);
    }

    // GET: PagesController/Create
    public async Task<ActionResult> CreateAsync()
    {
      try
      {
        LoggerHelper.LogInformation("Create işlemi başlatıldı");
        return View();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Create işlemi sırasında hata oluştu");
        throw;
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(PageDto pageDto, IFormFile? PageImagePath)
    {
      try
      {
        LoggerHelper.LogInformation("Create işlemi başlatıldı");

        string filePath = null;
        if (PageImagePath != null)
        {
          filePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
        }

        await _service.CreateAsync(pageDto, filePath, pageDto.PageVisibility);

        LoggerHelper.LogInformation("Create işlemi başarıyla tamamlandı");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Create işlemi sırasında hata oluştu");
        ModelState.AddModelError("", "Hata Oluştu!");
        return View(pageDto);
      }
    }

    // GET: PagesController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
      try
      {
        var pageDto = await _service.GetPageByIdAsync(id);
        if (pageDto == null)
        {
          LoggerHelper.LogError(null, "Edit işlemi sırasında sayfa bulunamadı {Id}", id);
          return NotFound();
        }

        LoggerHelper.LogInformation("Edit işlemi başlatıldı {Id}", id);
        return View(pageDto);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Edit işlemi sırasında hata oluştu {Id}", id);
        throw;
      }
    }

    // POST: PagesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, PageDto pageDto, IFormFile? PageImagePath)
    {
      try
      {
        LoggerHelper.LogInformation("Edit işlemi başlatıldı {Id}", id);

        if (id != pageDto.Id)
        {
          LoggerHelper.LogError(null, "Edit işlemi sırasında hata oluştu - Id'ler eşleşmiyor {Id}", id);
          return NotFound();
        }

        string filePath = null;
        if (PageImagePath is not null)
        {
          filePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
        }

        await _service.UpdateAsync(pageDto, filePath, pageDto.PageVisibility);

        LoggerHelper.LogInformation("Edit işlemi başarıyla tamamlandı {Id}", id);
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Edit işlemi sırasında hata oluştu {Id}", id);
        ModelState.AddModelError("", "Hata Oluştu!");
        return View(pageDto);
      }
    }

    // GET: PagesController/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Delete işlemi başlatıldı  {Id}", id);
        var pageDto = await _service.GetPageByIdAsync(id);
        if (pageDto == null)
        {
          LoggerHelper.LogError(null, "Delete işlemi sırasında sayfa bulunamadı  {Id}", id);
          return NotFound();
        }

        return View(pageDto);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Delete işlemi sırasında hata oluştu  {Id}", id);
        throw;
      }
    }

    // POST: PagesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      try
      {
        LoggerHelper.LogInformation("Delete işlemi başlatıldı {Id}", id);
        await _service.DeleteAsync(id);
        LoggerHelper.LogInformation("Delete işlemi başarıyla tamamlandı {Id}", id);
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Delete işlemi sırasında hata oluştu {Id}", id);
        throw;
      }
    }
  }
}
