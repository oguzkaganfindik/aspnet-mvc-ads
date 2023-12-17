using Ads.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertImagesController : Controller
    {
        private readonly IAdvertImageService _service;

        public AdvertImagesController(IAdvertImageService service)
        {
            _service = service;
        }

        // GET: AdvertImagesController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomAdvertImageList();
            return View(model);
        }

        // GET: AdvertImagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvertImagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertImagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvertImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertImagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvertImagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
