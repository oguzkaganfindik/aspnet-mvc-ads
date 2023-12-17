using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
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

        // GET: AdvertImagesController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomAdvertImageList();
            return View(model);
        }

        // GET: AdvertImagesController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvertImagesController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: AdvertImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AdvertImage advertImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(advertImage);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View(advertImage);
        }

        // GET: AdvertImagesController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View(model);
        }

        // POST: AdvertImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, AdvertImage advertImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(advertImage);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View(advertImage);
        }

        // GET: AdvertImagesController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: AdvertImagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, AdvertImage advertImage)
        {
            try
            {
                _service.Delete(advertImage);
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
