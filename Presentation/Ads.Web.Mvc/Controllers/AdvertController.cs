
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Controllers
{
    public class AdvertController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
