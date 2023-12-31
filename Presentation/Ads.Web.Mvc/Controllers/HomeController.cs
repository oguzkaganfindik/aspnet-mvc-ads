using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.ViewModels;
using Ads.Web.Mvc.Extensions;
using Ads.Web.Mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Ads.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
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
            return View(new Persistence.ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}