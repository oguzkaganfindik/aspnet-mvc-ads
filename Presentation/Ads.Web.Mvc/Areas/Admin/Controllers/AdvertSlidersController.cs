using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertSlidersController : Controller
    {
        private readonly IService<AdvertSliderImage> _service;
        private readonly IService<Advert> _serviceAdvert;

        public AdvertSlidersController(IService<AdvertSliderImage> service, IService<Advert> serviceAdvert)
        {
            _service = service;
            _serviceAdvert = serviceAdvert;
        }


        // GET: SlidersController
        public async Task<IActionResult> Index()
        {
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: SlidersController/Details/5
        public async Task<IActionResult> DetailsAsync(int id)
        {
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View();
        }

        // GET: SlidersController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AdvertSliderImage collection, IFormFile? AdvertSliderImagePath)
        {
            try
            {
                collection.AdvertSliderImagePath = await FileHelper.FileLoaderAsync(AdvertSliderImagePath, "/Img/AdvertSliderImages/");
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

        // GET: SlidersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.FindAsync(id);
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View(data);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdvertSliderImage collection, IFormFile? AdvertSliderImagePath)
        {
            try
            {
                if (AdvertSliderImagePath is not null)
                {
                    collection.AdvertSliderImagePath = await FileHelper.FileLoaderAsync(AdvertSliderImagePath, "/Img/AdvertSliderImages/");
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

        // GET: SlidersController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _service.FindAsync(id);
            return View(data);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, AdvertSliderImage collection)
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
