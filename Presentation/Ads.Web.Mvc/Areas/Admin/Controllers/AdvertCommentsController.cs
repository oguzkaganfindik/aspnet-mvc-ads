using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Web.Mvc.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
    [Area("Admin")]
    public class AdvertCommentsController : Controller
    {
        private readonly IAdvertCommentService _service;
        private readonly IAdvertService _serviceAdvert;
        private readonly UserManager<AppUser> _userManager;

        public AdvertCommentsController(UserManager<AppUser> userManager, IAdvertCommentService service, IAdvertService serviceAdvert)
        {

            _userManager = userManager;
            _service = service;
            _serviceAdvert = serviceAdvert;
        }

        // GET: AdvertCommentsController/Create
        public async Task<IActionResult> CreateAsync(int advertId)
        {
            var users = _userManager.Users.ToList();
            var viewModel = new AdvertCommentCreationViewModel
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

        // POST: AdvertCommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdvertCommentCreationViewModel viewModel)
        {
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

            var advertCommentDto = new AdvertCommentDto
            {
                AdvertId = viewModel.AdvertId,
                UserId = viewModel.UserId.Value,
                Comment = viewModel.Comment,
                IsActive =viewModel.IsActive,
            };

            await _service.AddAdvertCommentAsync(advertCommentDto);

            return RedirectToAction("Details", "Adverts", new { id = advertCommentDto.AdvertId });
        }

        //// GET: AdvertCommentsController/Edit/5
        //public async Task<IActionResult> EditAsync(int id)
        //{
        //    var model = await _service.FindAsync(id);
        //    ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
        //    ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
        //    return View(model);
        //}

        //// POST: AdvertCommentsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAsync(int id, AdvertComment advertComment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _service.Update(advertComment);
        //            await _service.SaveAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            ModelState.AddModelError("", "Hata Oluştu!");
        //        }
        //    }
        //    ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
        //    ViewBag.UserId = new SelectList(await _userManager.Users.ToListAsync(), "Id", "Username");
        //    return View(advertComment);
        //}

        //// GET: AdvertCommentsController/Delete/5
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    var model = await _service.FindAsync(id);
        //    return View(model);
        //}

        //// POST: AdvertCommentsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteAsync(int id, AdvertComment advertComment)
        //{
        //    try
        //    {
        //        _service.Delete(advertComment);
        //        await _service.SaveAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}



//using Ads.Application.Services;
//using Ads.Domain.Entities.Concrete;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace Ads.Web.Mvc.Areas.Admin.Controllers
//{
//    //[Area("Admin")]
//    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
//    public class AdvertCommentsController : Controller
//    {
//        private readonly IAdvertCommentService _service;
//        private readonly IService<Advert> _serviceAdvert;
//        private readonly IService<AppUser> _serviceUser;


//        public AdvertCommentsController(IAdvertCommentService service, IService<Advert> serviceAdvert, IService<AppUser> serviceUser)
//        {
//            _service = service;
//            _serviceAdvert = serviceAdvert;
//            _serviceUser = serviceUser;
//        }


//        // GET: AdvertCommentsController
//        public async Task<IActionResult> IndexAsync()
//        {
//            var model = await _service.GetCustomAdvertCommentList();
//            return View(model);
//        }

//        // GET: AdvertCommentsController/Details/5
//        public IActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: AdvertCommentsController/Create
//        public async Task<IActionResult> CreateAsync()
//        {
//            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            return View();
//        }

//        // POST: AdvertCommentsController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> CreateAsync(AdvertComment advertComment)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _service.AddAsync(advertComment);
//                    await _service.SaveAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch
//                {
//                    ModelState.AddModelError("", "Hata Oluştu!");
//                }
//            }
//            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            return View(advertComment);
//        }

//        // GET: AdvertCommentsController/Edit/5
//        public async Task<IActionResult> EditAsync(int id)
//        {
//            var model = await _service.FindAsync(id);
//            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            return View(model);
//        }

//        // POST: AdvertCommentsController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditAsync(int id, AdvertComment advertComment)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _service.Update(advertComment);
//                    await _service.SaveAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch
//                {
//                    ModelState.AddModelError("", "Hata Oluştu!");
//                }
//            }
//            ViewBag.AdvertId = new SelectList(await _serviceAdvert.GetAllAsync(), "Id", "Title");
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            return View(advertComment);
//        }

//        // GET: AdvertCommentsController/Delete/5
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var model = await _service.FindAsync(id);
//            return View(model);
//        }

//        // POST: AdvertCommentsController/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteAsync(int id, AdvertComment advertComment)
//        {
//            try
//            {
//                _service.Delete(advertComment);
//                await _service.SaveAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
