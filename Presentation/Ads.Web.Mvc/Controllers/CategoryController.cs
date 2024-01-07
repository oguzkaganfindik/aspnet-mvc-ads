using Ads.Application.DTOs.Category;
using Ads.Application.Services;
using Ads.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _service;

		public CategoryController(ICategoryService service)
		{
			_service = service;
		}

		// GET: CategoryController
		public async Task<IActionResult> IndexAsync()
		{
			var model = await _service.GetAllCategories();
			return View(model);
		}

        // GET: CategoryController/Detail
        public async Task<IActionResult> Details(int id)
        {
            var model = await _service.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var modelDto = new CategoryDto
            {
                Id = model.Id,
                Name = model.Name,
            };

            return View(new List<CategoryDto> { modelDto });
        }

        
	}
}
