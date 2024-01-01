using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "AdminPolicy")]
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly IPageService _service;
        private readonly ILogger<PagesController> _logger;

        public PagesController(IPageService service, ILogger<PagesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: PagesController
        public async Task<ActionResult> Index()
        {
            List<PageDto> pageDtos = await _service.GetAllPagesWithSettingsAsync();

            return View(pageDtos);

        }

        // GET: PagesController/Create
        public async Task<IActionResult> CreateAsync()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(PageDto pageDto, IFormFile? PageImagePath)
        {
            try
            {
                string filePath = null;
                if (PageImagePath != null)
                {
                    filePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
                }

                await _service.CreateAsync(pageDto, filePath, pageDto.PageVisibility);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Hata durumunda kullanıcıyı aynı sayfada tut
                return View(pageDto);
            }
        }


        // GET: PagesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pageDto = await _service.GetPageByIdAsync(id); // Bu metod sayfa bilgilerini getirir
            if (pageDto == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Page Visibility: {PageVisibility}", pageDto.PageVisibility);

            ModelState.Clear();
            return View(pageDto);
        }

        // POST: PagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PageDto pageDto, IFormFile? PageImagePath)
        {
            if (id != pageDto.Id)
            {
                return NotFound();
            }

            try
            {
                string filePath = null;
                if (PageImagePath is not null)
                {
                    filePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
                }

                await _service.UpdateAsync(pageDto, filePath, pageDto.PageVisibility);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Hata durumunda kullanıcıyı aynı sayfada tut
                return View(pageDto);
            }
        }

        // GET: PagesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pageDto = await _service.GetPageByIdAsync(id);
            if (pageDto == null)
            {
                return NotFound();
            }

            return View(pageDto);
        }


        // POST: PagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
