using Ads.Application.Services;
using Ads.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly INavbarService _navbarService;


        public NavbarViewComponent(INavbarService navbarService)
        {
            _navbarService = navbarService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = await _navbarService.GetNavbarViewModelAsync();
            return View(viewModel);
        }
    }
}
