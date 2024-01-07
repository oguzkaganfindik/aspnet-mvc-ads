using Ads.Application.DTOs.AdvertRating;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Web.Mvc.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "Admin")]
  
    public class AdvertRatingsController : Controller
    {
        private readonly IAdvertRatingService _service;
        private readonly IAdvertService _serviceAdvert;
        private readonly UserManager<AppUser> _userManager;

        public AdvertRatingsController(UserManager<AppUser> userManager, IAdvertRatingService service, IAdvertService serviceAdvert)
        {

            _userManager = userManager;
            _service = service;
            _serviceAdvert = serviceAdvert;
        }

        // GET: AdvertRatingsController/Create
        public ActionResult Create(int advertId)
        {           
            var users = _userManager.Users.ToList();
            var viewModel = new AdvertRatingCreationViewModel
            {
                AdvertId = advertId,
                Users = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: AdvertRatingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdvertRatingCreationViewModel viewModel)
        {
            // Kullanıcının bu ilana daha önce oy verip vermediğini kontrol edin
            var existingRating = await _service.GetByUserIdAndAdvertIdAsync(viewModel.UserId.Value, viewModel.AdvertId);
            if (existingRating != null)
            {
                // Eğer kullanıcı bu ilana daha önce oy vermişse bir hata mesajı ekle
                ModelState.AddModelError(string.Empty, "You have already rated this advert.");
            }

            if (!ModelState.IsValid)
            {
                // Kullanıcı listesini yeniden yükleyin
                viewModel.Users = await _userManager.Users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToListAsync();

                // View'a geri dön
                return View(viewModel);
            }

            var advertRatingDto = new AdvertRatingDto
            {
                AdvertId = viewModel.AdvertId,
                UserId = viewModel.UserId.Value, // Burada UserId'nin null olmadığından eminiz
                Rating = viewModel.Rating
            };

            // Veri tabanına yeni advert rating ekleyin
            await _service.AddAdvertRatingAsync(advertRatingDto);

            // İşlem başarılıysa kullanıcıyı Adverts controller'ın Details view'ına yönlendirin
            return RedirectToAction("Details", "Adverts", new { id = advertRatingDto.AdvertId });
        }



        //// GET: AdvertRatingsController/Edit/5
        //public async Task<IActionResult> EditAsync(int id)
        //{
        //    var model = await _service.FindAsync(id);
        //    ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
        //    ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");

        //    return View(model);
        //}

        //// POST: AdvertRatingsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAsync(int id, AdvertRating advertRating)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _service.Update(advertRating);
        //            await _service.SaveAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            ModelState.AddModelError("", "Hata Oluştu!");
        //        }
        //    }
        //    ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
        //    ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");

        //    return View(advertRating);
        //}

        // POST: AdvertRatingsController/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int userid, int advertid)
        {
            try
            {
                var advertId = await _service.DeleteAdvertRatingAsync(userid, advertid);
                if (advertId == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Details", "Adverts", new { id = advertId, area = "Admin" });




            }
            catch (Exception ex)
            {
                // Hata yönetimi
                TempData["Error"] = "Rating silinirken bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index", "Adverts");
            }
        }

    }
}