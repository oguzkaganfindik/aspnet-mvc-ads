using Ads.Application.DTOs.CategoryAdvert;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
    public class CategoryAdvertDtoValidator : AbstractValidator<CategoryAdvertDto>
	{
		public CategoryAdvertDtoValidator()
		{
			RuleFor(dto => dto.CategoryId)
				.NotEmpty().WithMessage("Category ID is required.")
				.GreaterThan(0).WithMessage("Category ID must be greater than zero.");

			RuleFor(dto => dto.AdvertId)
				.NotEmpty().WithMessage("Advert ID is required.")
				.GreaterThan(0).WithMessage("Advert ID must be greater than zero.");			
		}
	}
}
