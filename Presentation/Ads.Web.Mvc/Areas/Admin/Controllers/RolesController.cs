using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IService<Role> _service;

        public RolesController(IService<Role> service)
        {
            _service = service;
        }

        // GET: RolesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role rol)
        {
            try
            {
                _service.Add(rol);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Role rol)
        {
            try
            {
                _service.Update(rol);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Role rol)
        {
            try
            {
                _service.Delete(rol);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
