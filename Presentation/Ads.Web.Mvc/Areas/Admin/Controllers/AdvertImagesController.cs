using Ads.Application.DTOs.AdvertImage;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
    [Area("Admin")]
    public class AdvertImagesController : Controller
    {
        private readonly IAdvertImageService _service;
        private readonly IService<Advert> _serviceAdvert;

        public AdvertImagesController(IAdvertImageService service, IService<Advert> serviceAdvert)
        {
            _service = service;
            _serviceAdvert = serviceAdvert;
        }

        // GET: AdvertImagesController/Create
        public async Task<IActionResult> Create(int advertId)
        {
            var advertImageDto = new AdvertImageDto { AdvertId = advertId };
            return View(advertImageDto);
        }

        // POST: AdvertImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] AdvertImageDto advertImageDto)
        {
            if (!ModelState.IsValid || advertImageDto.File == null)
            {
                return View(advertImageDto);
            }

            var filePath = await FileHelper.FileLoaderAsync(advertImageDto.File, "/Img/AdvertImages/");
            advertImageDto.AdvertImagePath = filePath;

            await _service.AddAdvertImageAsync(advertImageDto);
            return RedirectToAction("Details", "Adverts", new { id = advertImageDto.AdvertId });
        }

        // POST: AdvertImagesController/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var advertId = await _service.DeleteAdvertImageAsync(id);
                if (advertId == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Details", "Adverts", new { id = advertId.Value, area = "Admin" });

            }
            catch (Exception ex)
            {
                // Hata yönetimi
                TempData["Error"] = "Resim silinirken bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index", "Adverts");
            }
        }


    }
}
