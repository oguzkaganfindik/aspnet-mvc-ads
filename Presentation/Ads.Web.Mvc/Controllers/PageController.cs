using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ads.Web.Mvc.Controllers
{
    public class PageController : Controller
    {
        private readonly IService<Page> _pageService;

        public PageController(IService<Page> service)
        {
            _pageService = service;
        }

        public IActionResult Index()
        {
            var pages = _pageService.GetAll();
            return View(pages);
        }

        public IActionResult AboutUs()
        {
            var pages = _pageService.GetAll();
            return View(pages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
