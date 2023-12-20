using Ads.Application.DTOs.AdvertImage;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class AdvertImagesController : Controller
    {
        private readonly IAdvertImageService _service;
        private readonly IService<Advert> _serviceAdvert;
        private readonly IMapper _mapper;

        public AdvertImagesController(IAdvertImageService service, IService<Advert> serviceAdvert, IMapper mapper)
        {
            _service = service;
            _serviceAdvert = serviceAdvert;
            _mapper = mapper;
        }

        // GET: AdvertImagesController
        public async Task<IActionResult> IndexAsync()
        {
            var advertImages = await _service.GetAllAsync();
            var model = _mapper.Map<IEnumerable<AdvertImageDto>>(advertImages);
            return View(model);
        }

        // GET: AdvertImagesController/Details/5
        public async Task<IActionResult> DetailsAsync(int id)
        {
            var advertImage = await _service.FindAsync(id);
            var model = _mapper.Map<AdvertImageDto>(advertImage);
            return View(model);
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
        public async Task<IActionResult> CreateAsync(AdvertImageDto advertImageDTO, IFormFile? AdvertImagePath)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
                return View(advertImageDTO);
            }

            try
            {
                var advertImage = _mapper.Map<AdvertImage>(advertImageDTO);
                advertImage.AdvertImagePath = await FileHelper.FileLoaderAsync(AdvertImagePath, "/Img/AdvertImages/");
                await _service.AddAsync(advertImage);
                await _service.SaveAsync();
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
                return View(advertImageDTO);
            }
        }
    

    // GET: AdvertImagesController/Edit/5
    public async Task<IActionResult> EditAsync(int id)
        {
            var advertImage = await _service.FindAsync(id);
            var model = _mapper.Map<AdvertImageDto>(advertImage);
            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
            return View(model);
        }

        // POST: AdvertImagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, AdvertImageDto advertImageDTO, IFormFile? AdvertImagePath)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
                return View(advertImageDTO);
            }

            try
            {
                var advertImage = await _service.FindAsync(id);
                _mapper.Map(advertImageDTO, advertImage);

                if (AdvertImagePath != null)
                {
                    advertImage.AdvertImagePath = await FileHelper.FileLoaderAsync(AdvertImagePath, "/Img/AdvertImages/");
                }
                await _service.SaveAsync();
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
                return View(advertImageDTO);
            }
        }

        // GET: AdvertImagesController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var advertImage = await _service.FindAsync(id);
            var model = _mapper.Map<AdvertImageDto>(advertImage);
            return View(model);
        }

        // POST: AdvertImagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            var advertImage = await _service.FindAsync(id);
            _service.Delete(advertImage);
            await _service.SaveAsync();
            return RedirectToAction(nameof(IndexAsync));
        }
    }
}

