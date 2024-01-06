using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Infrastructure.Services;
using Ads.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "Admin")]

  public class PagesController : Controller
  {
    private readonly IPageService _service;

    public PagesController(IPageService service)
    {
      _service = service;
    }

    public async Task<ActionResult> Index()
    {

      List<PageDto> pageDtos = await _service.GetAllPagesWithSettingsAsync();

      return View(pageDtos);


    }

    public IActionResult Create()
    {
      try
      {
        LoggerHelper.LogInformation("Yeni sayfa oluşturma sayfası görüntülendi.");
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
    public async Task<IActionResult> Create(PageDto pageDto, IFormFile? PageImagePath)
    {
      try
      {
        string filePath = null;
        if (PageImagePath != null)
        {
          filePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
        }

        await _service.CreateAsync(pageDto, filePath, pageDto.PageVisibility);
        LoggerHelper.LogInformation("Yeni sayfa başarıyla oluşturuldu. Sayfa Adı: {PageName}");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Yeni sayfa oluşturulurken bir hata oluştu.");
        ModelState.AddModelError("", "Hata Oluştu!");
        return View(pageDto);
      }
    }

    public async Task<IActionResult> Edit(int id)
    {
      try
      {
        var pageDto = await _service.GetPageByIdAsync(id);
        if (pageDto == null)
        {
          LoggerHelper.LogWarning("Düzenleme için sayfa bulunamadı.  {PageId}", id);
          return NotFound();
        }

        LoggerHelper.LogInformation("Sayfa görüntüleme sayfası açıldı.  {PageId}, Sayfa Adı: {PageName}", pageDto.Id);

        ModelState.Clear();
        return View(pageDto);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Sayfa düzenleme sayfasını görüntüleme sırasında bir hata oluştu.  {PageId}", id);
        throw;
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PageDto pageDto, IFormFile? PageImagePath)
    {
      try
      {
        if (id != pageDto.Id)
        {
          LoggerHelper.LogWarning("Düzenleme için geçersiz sayfa Id.");
          return NotFound();
        }

        string filePath = null;
        if (PageImagePath is not null)
        {
          filePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
        }

        await _service.UpdateAsync(pageDto, filePath, pageDto.PageVisibility);
        LoggerHelper.LogInformation("Sayfa düzenleme işlemi başarıyla tamamlandı.  {PageName}");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Sayfa düzenleme sırasında bir hata oluştu. {PageId}", pageDto.Id);
        ModelState.AddModelError("", "Hata Oluştu!");
        return View(pageDto);
      }
    }

    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var pageDto = await _service.GetPageByIdAsync(id);
        if (pageDto == null)
        {
          LoggerHelper.LogWarning("Silme için sayfa bulunamadı.  {PageId}", id);
          return NotFound();
        }

        LoggerHelper.LogInformation("Sayfa silme sayfası açıldı.  {PageId}, Sayfa Adı: {PageName}", pageDto.Id);
        return View(pageDto);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Sayfa silme sayfasını açarken bir hata oluştu.  {PageId}", id);
        throw;
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      try
      {
        await _service.DeleteAsync(id);
        LoggerHelper.LogInformation("Sayfa silme işlemi başarıyla tamamlandı.");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "Sayfa silme işlemi sırasında bir hata oluştu.");
        throw;
      }
    }
  }
}
