using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class AdvertsController : Controller
    {
        private readonly IAdvertService _service;
        private readonly IService<User> _serviceUser;
        private readonly IService<Category> _serviceCategory;
        private readonly IService<SubCategory> _serviceSubCategory;
        private readonly IService<AdvertComment> _serviceAdvertComment;
        private readonly IService<AdvertImage> _serviceAdvertImage;


        public AdvertsController(IAdvertService service, IService<User> serviceUser, IService<Category> serviceCategory, IService<SubCategory> serviceSubCategory, IService<AdvertComment> serviceAdvertComment, IService<AdvertImage> serviceAdvertImage)
        {
            _service = service;
            _serviceUser = serviceUser;
            _serviceCategory = serviceCategory;
            _serviceSubCategory = serviceSubCategory;
            _serviceAdvertComment = serviceAdvertComment;
            _serviceAdvertImage = serviceAdvertImage;
        }

        // GET: AdvertsController
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
            ViewBag.AdvertComment = new SelectList(await _serviceAdvertComment.GetAllAsync(), "Id", "Comment");
            var model = await _service.GetCustomAdvertList();

            return View(model);
        }

        // GET: AdvertsController/Details/5
        public async Task<IActionResult> DetailsAsync(int id)
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
            ViewBag.AdvertComment = new SelectList(await _serviceAdvertComment.GetAllAsync(), "Id", "Comment");

            return View();
        }

        // GET: AdvertsController/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View();
        }

        // POST: AdvertsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Advert advert)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.AddAsync(advert);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View(advert);
        }

        // GET: AdvertsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View(model);
        }

        // POST: AdvertsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Advert advert)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(advert);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View(advert);
        }

        // GET: AdvertsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);

            return View(model);
        }

        // POST: AdvertsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, Advert advert)
        {
            try
            {
                _service.Delete(advert);
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
