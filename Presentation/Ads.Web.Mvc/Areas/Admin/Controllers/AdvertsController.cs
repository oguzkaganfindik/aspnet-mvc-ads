using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertImage;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
    [Area("Admin")]
    public class AdvertsController : Controller
    {
        private readonly IAdvertService _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IService<Category> _serviceCategory;
        private readonly IService<SubCategory> _serviceSubCategory;
        private readonly IService<AdvertComment> _serviceAdvertComment;
        private readonly IService<AdvertImage> _serviceAdvertImage;
        private readonly IService<AdvertRating> _serviceAdvertRating;
        private readonly IMapper _mapper;
        private readonly ILogger<AdvertsController> _logger;


        public AdvertsController(IAdvertService service, UserManager<AppUser> userManager, IService<Category> serviceCategory, IService<SubCategory> serviceSubCategory, IService<AdvertComment> serviceAdvertComment, IService<AdvertImage> serviceAdvertImage, IMapper mapper, IService<AdvertRating> serviceAdvertRating, ILogger<AdvertsController> logger)
        {
            _service = service;
            _userManager = userManager;
            _serviceCategory = serviceCategory;
            _serviceSubCategory = serviceSubCategory;
            _serviceAdvertComment = serviceAdvertComment;
            _serviceAdvertImage = serviceAdvertImage;
            _serviceAdvertRating = serviceAdvertRating;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: AdvertsController
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
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
            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
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
            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View();
        }

        // POST: AdvertsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = _mapper.Map<Advert>(advertDto);

                    model.CategoryAdverts = selectedCategoryIds.Select(id => new CategoryAdvert { CategoryId = id }).ToList();
                    model.SubCategoryAdverts = selectedSubCategoryIds.Select(id => new SubCategoryAdvert { SubCategoryId = id }).ToList();

                    await _service.AddAsync(model);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

            return View(advertDto);
        }

        //GET: AdvertsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var advert = await _service.GetCustomAdvert(id);
            if (advert == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<AdvertDto>(advert);

            // Kategori ve alt kategori ID'lerini List<int> olarak ayarlayın
            model.SelectedCategoryIds = advert.CategoryAdverts?.Select(c => c.CategoryId).ToList() ?? new List<int>();
            model.SelectedSubCategoryIds = advert.SubCategoryAdverts?.Select(sc => sc.SubCategoryId).ToList() ?? new List<int>();

            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
            ViewBag.Categories = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name", model.SelectedCategoryIds);
            ViewBag.SubCategories = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name", model.SelectedSubCategoryIds);

            return View(model);
        }


        // POST: AdvertsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds)
        {
            if (!ModelState.IsValid)
            {
                // ModelState hatalarını logla
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    //logger.LogError(error.ErrorMessage); // logger, loglama için kullanılan bir nesnedir
                }

                return View(advertDto);
            }

            try
            {
                var advert = await _service.GetCustomAdvert(id);
                if (advert == null)
                {
                    _logger.LogWarning($"Advert with id {id} not found"); // İlan bulunamadığında loglama
                    return NotFound();
                }

                _logger.LogInformation($"Updating advert: {advert.Id}"); // Güncelleme işlemi başlamadan önce loglama
                _mapper.Map(advertDto, advert);

                // Kategori ve alt kategori ilişkilerini güncelle
                _logger.LogInformation("Updating category and subcategory relations");
                advert.CategoryAdverts.Clear();
                advert.SubCategoryAdverts.Clear();
                foreach (var categoryId in selectedCategoryIds)
                {
                    advert.CategoryAdverts.Add(new CategoryAdvert { CategoryId = categoryId, AdvertId = id });
                }
                foreach (var subCategoryId in selectedSubCategoryIds)
                {
                    advert.SubCategoryAdverts.Add(new SubCategoryAdvert { SubCategoryId = subCategoryId, AdvertId = id });
                }

                await _service.UpdateAsync(advert);
                await _service.SaveAsync();
                _logger.LogInformation($"Advert updated successfully: {advert.Id}"); // Güncelleme başarılı olduğunda loglama

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating advert: {ex}"); // Hata oluştuğunda detaylı loglama
                ModelState.AddModelError("", "Hata Oluştu: " + ex.Message);
            }

            ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
            ViewBag.Categories = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name", selectedCategoryIds);
            ViewBag.SubCategories = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name", selectedSubCategoryIds);

            return View(advertDto);
        }

        // GET: AdvertsController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var advert = await _service.GetCustomAdvert(id);
            if (advert == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<AdvertDto>(advert);
            return View(model);
        }

        // POST: AdvertsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var advert = await _service.FindAsync(id);
            if (advert != null)
            {
                _service.Delete(advert);
                await _service.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCategories = await _serviceSubCategory.GetAllAsync(sc => sc.CategoryId == categoryId);
            var result = subCategories.Select(sc => new { value = sc.Id, text = sc.Name }).ToList();

            return Json(result);
        }



    }
}

//using Ads.Application.DTOs.Advert;
//using Ads.Application.DTOs.AdvertImage;
//using Ads.Application.Services;
//using Ads.Domain.Entities.Concrete;
//using Ads.Web.Mvc.Areas.Admin.Models;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace Ads.Web.Mvc.Areas.Admin.Controllers
//{
//    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
//    //[Area("Admin")]
//    public class AdvertsController : Controller
//    {
//        private readonly IAdvertService _service;
//        private readonly IService<AppUser> _serviceUser;
//        private readonly IService<Category> _serviceCategory;
//        private readonly IService<SubCategory> _serviceSubCategory;
//        private readonly IService<AdvertComment> _serviceAdvertComment;
//        private readonly IService<AdvertImage> _serviceAdvertImage;
//        private readonly IService<AdvertRating> _serviceAdvertRating;
//        private readonly IMapper _mapper;
//        private readonly ILogger<AdvertsController> _logger;


//        public AdvertsController(IAdvertService service, IService<AppUser> serviceUser, IService<Category> serviceCategory, IService<SubCategory> serviceSubCategory, IService<AdvertComment> serviceAdvertComment, IService<AdvertImage> serviceAdvertImage, IMapper mapper, IService<AdvertRating> serviceAdvertRating, ILogger<AdvertsController> logger)
//        {
//            _service = service;
//            _serviceUser = serviceUser;
//            _serviceCategory = serviceCategory;
//            _serviceSubCategory = serviceSubCategory;
//            _serviceAdvertComment = serviceAdvertComment;
//            _serviceAdvertImage = serviceAdvertImage;
//            _serviceAdvertRating = serviceAdvertRating;
//            _mapper = mapper;
//            _logger = logger;
//        }

//        // GET: AdvertsController
//        public async Task<IActionResult> IndexAsync()
//        {
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
//            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
//            ViewBag.AdvertComment = new SelectList(await _serviceAdvertComment.GetAllAsync(), "Id", "Comment");

//            var advert = await _service.GetCustomAdvertList();
//            var model = _mapper.Map<IEnumerable<AdvertDto>>(advert);

//            return View(model);
//        }

//        // GET: AdvertsController/Details/5
//        public async Task<IActionResult> DetailsAsync(int id)
//        {
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
//            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");
//            ViewBag.AdvertComment = new SelectList(await _serviceAdvertComment.GetAllAsync(), "Id", "Comment");
//            var advert = await _service.FindAsync(id);
//            var model = _mapper.Map<AdvertImageDto>(advert);
//            return View(model);
//        }

//        // GET: AdvertsController/Create
//        public async Task<IActionResult> CreateAsync()
//        {
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
//            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

//            return View();
//        }

//        // POST: AdvertsController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> CreateAsync(AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var model = _mapper.Map<Advert>(advertDto);

//                    model.CategoryAdverts = selectedCategoryIds.Select(id => new CategoryAdvert { CategoryId = id }).ToList();
//                    model.SubCategoryAdverts = selectedSubCategoryIds.Select(id => new SubCategoryAdvert { SubCategoryId = id }).ToList();

//                    await _service.AddAsync(model);
//                    await _service.SaveAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch
//                {
//                    ModelState.AddModelError("", "Hata Oluştu!");
//                }
//            }
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            ViewBag.CategoryId = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name");
//            ViewBag.SubCategoryId = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name");

//            return View(advertDto);
//        }

//        //GET: AdvertsController/Edit/5
//        public async Task<IActionResult> EditAsync(int id)
//        {
//            var advert = await _service.GetCustomAdvert(id);
//            if (advert == null)
//            {
//                return NotFound();
//            }

//            var model = _mapper.Map<AdvertDto>(advert);

//            // Kategori ve alt kategori ID'lerini List<int> olarak ayarlayın
//            model.SelectedCategoryIds = advert.CategoryAdverts?.Select(c => c.CategoryId).ToList() ?? new List<int>();
//            model.SelectedSubCategoryIds = advert.SubCategoryAdverts?.Select(sc => sc.SubCategoryId).ToList() ?? new List<int>();

//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            ViewBag.Categories = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name", model.SelectedCategoryIds);
//            ViewBag.SubCategories = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name", model.SelectedSubCategoryIds);

//            return View(model);
//        }


//        // POST: AdvertsController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditAsync(int id, AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds)
//        {
//            if (!ModelState.IsValid)
//            {
//                // ModelState hatalarını logla
//                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//                {
//                    _logger.LogError(error.ErrorMessage); // _logger, loglama için kullanılan bir nesnedir
//                }

//                return View(advertDto);
//            }

//            try
//            {
//                var advert = await _service.GetCustomAdvert(id);
//                if (advert == null)
//                {
//                    _logger.LogWarning($"Advert with id {id} not found"); // İlan bulunamadığında loglama
//                    return NotFound();
//                }

//                _logger.LogInformation($"Updating advert: {advert.Id}"); // Güncelleme işlemi başlamadan önce loglama
//                _mapper.Map(advertDto, advert);

//                // Kategori ve alt kategori ilişkilerini güncelle
//                _logger.LogInformation("Updating category and subcategory relations");
//                advert.CategoryAdverts.Clear();
//                advert.SubCategoryAdverts.Clear();
//                foreach (var categoryId in selectedCategoryIds)
//                {
//                    advert.CategoryAdverts.Add(new CategoryAdvert { CategoryId = categoryId, AdvertId = id });
//                }
//                foreach (var subCategoryId in selectedSubCategoryIds)
//                {
//                    advert.SubCategoryAdverts.Add(new SubCategoryAdvert { SubCategoryId = subCategoryId, AdvertId = id });
//                }

//                await _service.UpdateAsync(advert);
//                await _service.SaveAsync();
//                _logger.LogInformation($"Advert updated successfully: {advert.Id}"); // Güncelleme başarılı olduğunda loglama

//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error updating advert: {ex}"); // Hata oluştuğunda detaylı loglama
//                ModelState.AddModelError("", "Hata Oluştu: " + ex.Message);
//            }

//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            ViewBag.Categories = new SelectList(await _serviceCategory.GetAllAsync(), "Id", "Name", selectedCategoryIds);
//            ViewBag.SubCategories = new SelectList(await _serviceSubCategory.GetAllAsync(), "Id", "Name", selectedSubCategoryIds);

//            return View(advertDto);
//        }

//        // GET: AdvertsController/Delete/5
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var advert = await _service.GetCustomAdvert(id);
//            if (advert == null)
//            {
//                return NotFound();
//            }

//            var model = _mapper.Map<AdvertDto>(advert);
//            return View(model);
//        }

//        // POST: AdvertsController/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var advert = await _service.FindAsync(id);
//            if (advert != null)
//            {
//                _service.Delete(advert);
//                await _service.SaveAsync();
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        public async Task<IActionResult> GetSubCategories(int categoryId)
//        {
//            var subCategories = await _serviceSubCategory.GetAllAsync(sc => sc.CategoryId == categoryId);
//            var result = subCategories.Select(sc => new { value = sc.Id, text = sc.Name }).ToList();

//            return Json(result);
//        }



//    }
//}
