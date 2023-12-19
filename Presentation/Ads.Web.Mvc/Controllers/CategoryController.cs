using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index(int id, int Page)
        {
            return View();
        }
    }
}
