using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Models.Settings
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class PagesController : Controller
    {
        private readonly IService<Page> _service;

        public PagesController(IService<Page> service)
        {
            _service = service;
        }

        // GET: PagesController
        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: PagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Page collection, IFormFile? PageImagePath)
        {
            try
            {
                collection.PageImagePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await _service.FindAsync(id);
            return View(data);
        }

        // POST: PagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Page collection, IFormFile? PageImagePath)
        {
            try
            {
                if (PageImagePath is not null)
                {
                    collection.PageImagePath = await FileHelper.FileLoaderAsync(PageImagePath, "/Img/PageImages/");
                }
                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = await _service.FindAsync(id);
            return View(data);
        }

        // POST: PagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Page collection)
        {
            try
            {
                _service.Delete(collection);
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
