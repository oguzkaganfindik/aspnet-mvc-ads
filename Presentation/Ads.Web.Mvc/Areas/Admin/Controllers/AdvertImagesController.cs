using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
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
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            var model = await _service.GetCustomAdvertImageList();
            return View(model);
        }

        // GET: AdvertImagesController/Details/5
        public async Task<IActionResult> DetailsAsync(int id)
        {
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
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
        public async Task<IActionResult> CreateAsync(AdvertImage collection, IFormFile? AdvertImagePath)
        {
            try
            {
                collection.AdvertImagePath = await FileHelper.FileLoaderAsync(AdvertImagePath, "/Img/AdvertImages/");
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertImagesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.FindAsync(id);
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View(data);
        }

        // POST: AdvertImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdvertImage collection, IFormFile? AdvertImagePath)
        {
            try
            {
                if (AdvertImagePath is not null)
                {
                    collection.AdvertImagePath = await FileHelper.FileLoaderAsync(AdvertImagePath, "/Img/AdvertImages/");
                }
                _service.Update(collection);
                await _service.SaveAsync();
                ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertImagesController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _service.FindAsync(id);
            return View(data);
        }

        // POST: AdvertImagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, AdvertImage collection)
        {
            try
            {
                _service.Delete(collection);
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