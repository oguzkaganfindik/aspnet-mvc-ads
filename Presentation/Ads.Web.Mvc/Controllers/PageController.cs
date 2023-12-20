using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ads.Web.Mvc.Controllers
{
    public class PageController : Controller
    {
        private readonly IService<Page> _service;
        private readonly IAdvertService _serviceAdvert;

        public PageController(IService<Page> service, IAdvertService serviceAdvert)
        {
            _service = service;
            _serviceAdvert = serviceAdvert;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel()
            {
                Pages = await _service.GetAllAsync(),
                //Adverts = await _serviceAdvert.GetCustomAdvertList(a => a.Anasayfa)
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}