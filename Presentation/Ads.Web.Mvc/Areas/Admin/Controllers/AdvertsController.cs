using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertImage;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Models.Adverts
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
        private readonly IService<AdvertRating> _serviceAdvertRating;
        private readonly IMapper _mapper;


        public AdvertsController(IAdvertService service, IService<User> serviceUser, IService<Category> serviceCategory, IService<SubCategory> serviceSubCategory, IService<AdvertComment> serviceAdvertComment, IService<AdvertImage> serviceAdvertImage, IMapper mapper, IService<AdvertRating> serviceAdvertRating)
        {
            _service = service;
            _serviceUser = serviceUser;
            _serviceCategory = serviceCategory;
            _serviceSubCategory = serviceSubCategory;
            _serviceAdvertComment = serviceAdvertComment;
            _serviceAdvertImage = serviceAdvertImage;
            _serviceAdvertRating = serviceAdvertRating;
            _mapper = mapper;
        }

        // GET: AdvertsController
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
            ViewBag.AdvertComment = new SelectList(await _serviceAdvertComment.GetAllAsync(), "Id", "Comment");
            
            var advert = await _service.GetCustomAdvertList();
            var model = _mapper.Map<IEnumerable<AdvertDto>>(advert);
            return View(model);
        }

        // GET: AdvertsController/Details/5
        public async Task<IActionResult> DetailsAsync(int id)
        {
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
            ViewBag.AdvertComment = new SelectList(await _serviceAdvertComment.GetAllAsync(), "Id", "Comment");
            var advert = await _service.FindAsync(id);
            var model = _mapper.Map<AdvertImageDto>(advert);
            return View(model);
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
        public async Task<IActionResult> CreateAsync(AdvertDto advertDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = _mapper.Map<Advert>(advertDto);
                    await _service.AddAsync(model);
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

            return View(advertDto);
        }

        // GET: AdvertsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var advert = await _service.FindAsync(id);
            var model = _mapper.Map<AdvertDto>(advert);
            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View(model);
        }

        // POST: AdvertsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, AdvertDto advertDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var advert = await _service.FindAsync(id);
                    var model = _mapper.Map(advertDto, advert);
                    _service.Update(model);
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

            return View(advertDto);
        }

        // GET: AdvertsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var advert = await _service.FindAsync(id);
            var model = _mapper.Map<AdvertDto>(advert);

            return View(model);
        }

        // POST: AdvertsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, AdvertDto advertDto)
        {
            try
            {
                var model = _mapper.Map<Advert>(advertDto);
                _service.Delete(model);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(advertDto);
            }
        }
    }
}
