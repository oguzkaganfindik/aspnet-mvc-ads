using Ads.Application.DTOs.Category;
using Ads.Application.Services;
using Ads.Application.ViewModels;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.Services
{
	public class CategoryService : CategoryRepository, ICategoryService
    {
		private readonly AppDbContext _context; 
		private readonly IMapper _mapper;
		public CategoryService(AppDbContext context, IMapper mapper) : base(context)
		{
			_context = context; 
			_mapper = mapper;
		}

		public async Task<IEnumerable<PopularCategoryViewModel>> GetPopularCategoriesAsync()
		{
			var categories = await _context.Categories
				.Include(c => c.CategoryAdverts)
					.ThenInclude(ca => ca.Advert)
						.ThenInclude(a => a.AdvertRatings)
				.OrderByDescending(c => c.CategoryAdverts
					.SelectMany(ca => ca.Advert.AdvertRatings)
					.Average(ar => (double?)ar.Rating) ?? 0.0)
				.Take(5)
				.ToListAsync();

			var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

			var popularCategories = categoryDtos.Select(dto => new PopularCategoryViewModel
			{
				CategoryId = dto.Id,
				CategoryName = dto.Name,
				AverageRating = dto.CategoryAdverts.Any()
					? Math.Round(dto.CategoryAdverts
						.SelectMany(ca => ca.Advert.AdvertRatings)
						.Average(ar => ar.Rating) ?? 0.0, 2) 
					: 0.0
			}).ToList();

			return popularCategories;
		}
	}
}