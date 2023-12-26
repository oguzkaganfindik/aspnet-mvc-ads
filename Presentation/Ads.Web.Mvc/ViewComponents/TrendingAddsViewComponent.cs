using Microsoft.AspNetCore.Mvc;
using Ads.Application.Services;
using System.Threading.Tasks;

namespace Ads.Web.Mvc.ViewComponents
{
    public class TrendingAddsViewComponent : ViewComponent
    {
        private readonly IAdvertService _advertService;

        public TrendingAddsViewComponent(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var trendingAdds = await _advertService.GetTrendingAddsAsync();
                return View(trendingAdds);
            }
            catch (Exception ex)
            {
                // Hata yönetimi - Loglama yapabilirsiniz.
                return View("Error", ex); // Hata durumunda bir hata sayfası göster
            }
        }
    }
}
