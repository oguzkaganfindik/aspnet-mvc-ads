using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IService<Slider> _service;

        public SlidersController(IService<Slider> service)
        {
            _service = service;
        }


        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider collection, IFormFile? ImagePath)
        {
            try
            {
                collection.ImagePath = await FileHelper.FileLoaderAsync(ImagePath, "/Img/Slider/");
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await _service.FindAsync(id);
            return View(data);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Slider collection, IFormFile? ImagePath)
        {
            try
            {
                if (ImagePath is not null)
                {
                    collection.ImagePath = await FileHelper.FileLoaderAsync(ImagePath, "/Img/Slider/");
                }
                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = await _service.FindAsync(id);
            return View(data);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Slider collection)
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
