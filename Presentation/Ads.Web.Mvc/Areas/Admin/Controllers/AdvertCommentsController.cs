using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "UserPolicy")]
    public class AdvertCommentsController : Controller
    {
        private readonly IAdvertCommentService _service;
        private readonly IService<Advert> _serviceAdvert;
        private readonly IService<User> _serviceUser;

        public AdvertCommentsController(IAdvertCommentService service, IService<Advert> serviceAdvert, IService<User> serviceUser)
        {
            _service = service;
            _serviceAdvert = serviceAdvert;
            _serviceUser = serviceUser;
        }


        // GET: AdvertCommentsController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetCustomAdvertCommentList();
            return View(model);
        }

        // GET: AdvertCommentsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvertCommentsController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View();
        }

        // POST: AdvertCommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AdvertComment advertComment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(advertComment);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View(advertComment);
        }

        // GET: AdvertCommentsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View(model);
        }

        // POST: AdvertCommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, AdvertComment advertComment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(advertComment);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            return View(advertComment);
        }

        // GET: AdvertCommentsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: AdvertCommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, AdvertComment advertComment)
        {
            try
            {
                _service.Delete(advertComment);
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
