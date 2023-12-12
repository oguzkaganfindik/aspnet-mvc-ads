using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
