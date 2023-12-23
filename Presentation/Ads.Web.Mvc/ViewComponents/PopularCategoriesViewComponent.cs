using Ads.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.ViewComponents
{
	public class PopularCategoriesViewComponent : ViewComponent
	{
		private readonly ICategoryService _categoryService;

		public PopularCategoriesViewComponent(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var popularCategories = await _categoryService.GetPopularCategoriesAsync();

			return View(popularCategories);
		}
	}
}
