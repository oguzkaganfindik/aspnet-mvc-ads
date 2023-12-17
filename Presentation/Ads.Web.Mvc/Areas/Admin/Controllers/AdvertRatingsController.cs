using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertRatingsController : Controller
    {

        // GET: AdvertRatingsController
        public async Task<ActionResult> IndexAsync()
        {
            return View();
        }

        // GET: AdvertRatingsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvertRatingsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertRatingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertRatingsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvertRatingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertRatingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvertRatingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
