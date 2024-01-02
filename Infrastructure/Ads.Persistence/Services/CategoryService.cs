using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.Page;
using Ads.Application.DTOs.SubCategory;
using Ads.Application.Services;
using Ads.Application.ViewModels;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.Services
{
	public class CategoryService : CategoryRepository, ICategoryService
    {
		private readonly IMapper _mapper;
		public CategoryService(AppDbContext context, IMapper mapper) : base(context)
		{
			_mapper = mapper;
		}


        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _context.Categories
                                       .Include(x => x.SubCategories)
									   .Include(y => y.CategoryAdverts)
									   .ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }


        public async Task<List<SubCategoryDto>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var subCategories = await _context.SubCategories
                                              .Where(sc => sc.CategoryId == categoryId)
                                              .ToListAsync();

            return _mapper.Map<List<SubCategoryDto>>(subCategories);
        }


        public async Task CreateAsync(CategoryDto categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);
            // CategoryIconPath yerine CategoryIcon kullanılıyor
            newCategory.CategoryIcon = categoryDto.CategoryIcon;

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {           
            var categoryToUpdate = await _context.Categories.FindAsync(categoryDto.Id);
            if (categoryToUpdate == null)
            {
                throw new ArgumentException("Category not found.");
            }
            _mapper.Map(categoryDto, categoryToUpdate);

            _context.Categories.Update(categoryToUpdate);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
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

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}