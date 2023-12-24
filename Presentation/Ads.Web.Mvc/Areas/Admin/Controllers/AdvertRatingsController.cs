using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "UserPolicy")]
    public class AdvertRatingsController : Controller
    {
        private readonly IAdvertRatingService _service;
        private readonly IService<Advert> _serviceAdvert;
        private readonly IService<User> _serviceUser;



        public AdvertRatingsController(IService<Advert> serviceAdvert, IService<User> serviceUser, IAdvertRatingService service)
        {
            _serviceAdvert = serviceAdvert;
            _serviceUser = serviceUser;
            _service = service;
        }

        // GET: AdvertRatingsController
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            var model = await _service.GetCustomAdvertRatingList();
            return View(model);
        }

        // GET: AdvertRatingsController/Details/5
        public async Task<IActionResult> DetailsAsync(int id)
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View();
        }

        // GET: AdvertRatingsController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: AdvertRatingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AdvertRating advertRating)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.AddAsync(advertRating);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");

            return View(advertRating);
        }

        // GET: AdvertRatingsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");

            return View(model);
        }

        // POST: AdvertRatingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, AdvertRating advertRating)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(advertRating);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");

            return View(advertRating);
        }

        // GET: AdvertRatingsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);

            return View(model);
        }

        // POST: AdvertRatingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, AdvertRating advertRating)
        {
            try
            {
                _service.Delete(advertRating);
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
