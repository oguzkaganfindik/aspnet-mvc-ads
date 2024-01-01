//using Ads.Application.DTOs.Role;
//using Ads.Application.Services;
//using Ads.Application.ViewModels;
//using Ads.Domain.Entities.Concrete;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace Ads.Web.Mvc.Areas.Admin.Controllers
//{
//    //[Area("Admin"), Authorize(Policy = "AdminPolicy")]
//    //[Area("Admin")]
//    public class RolesController : Controller
//    {
//        private readonly IService<Role> _service;   
//        private readonly IService<AppUser> _serviceUser;   
//        private readonly IMapper _mapper;

//        public RolesController(IService<Role> service, IMapper mapper, IService<AppUser> serviceUser)
//        {
//            _service = service;
//            _mapper = mapper;
//            _serviceUser = serviceUser;
//        }

//        // GET: RolesController
//        public async Task<IActionResult> IndexAsync()
//        {
//            var role = await _service.GetAllAsync();
//            var model = _mapper.Map<IEnumerable<RoleDto>>(role);
//            return View(model);
//        }

//        // GET: RolesController/Details/5
//        public async Task<IActionResult> DetailAsync(int id)
//        {
//            ViewBag.UserId = new SelectList(await _serviceUser.GetAllAsync(), "Id", "Username");
//            var role = await _service.FindAsync(id);
//            if (role == null)
//            {
//                return NotFound();
//            }

//            var roleDto = _mapper.Map<RoleDto>(role);
//            return View(roleDto);
//        }

//// GET: RolesController/Create
//public async Task<IActionResult> CreateAsync()
//{

//    return View();
//}

//// POST: RolesController/Create
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> CreateAsync(Role rol)
//{
//    try
//    {
//        _service.Add(rol);
//        _service.SaveAsync();
//        return RedirectToAction(nameof(Index));
//    }
//    catch
//    {
//        return View();
//    }
//}

//// GET: RolesController/Edit/5
//public async Task<IActionResult> EditAsync(int id)
//{
//    var model = await _service.FindAsync(id);
//    return View(model);
//}

//// POST: RolesController/Edit/5
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> EditAsync(int id, Role rol)
//{
//    try
//    {
//        _service.Update(rol);
//        _service.SaveAsync();
//        return RedirectToAction(nameof(Index));
//    }
//    catch
//    {
//        return View();
//    }
//}

//// GET: RolesController/Delete/5
//public async Task<IActionResult> DeleteAsync(int id)
//{
//    var model = await _service.FindAsync(id);
//    return View(model);
//}

//// POST: RolesController/Delete/5
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> DeleteAsync(int id, Role rol)
//{
//    try
//    {
//        _service.Delete(rol);
//        _service.SaveAsync();
//        return RedirectToAction(nameof(Index));
//    }
//    catch
//    {
//        return View();
//    }
//}
//    }
//}
