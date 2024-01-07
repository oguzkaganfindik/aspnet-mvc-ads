using Ads.Application.Services;
using Ads.Persistence.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.ViewComponents
{ 
    public class AllCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public AllCategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = await _categoryService.GetAllCategoriesViewModelAsync();
            return View(viewModel);
        }
    }
    
}
