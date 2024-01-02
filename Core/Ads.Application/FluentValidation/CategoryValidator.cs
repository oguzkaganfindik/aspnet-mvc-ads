using Ads.Application.DTOs.Category;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .Length(3, 200).WithMessage("Description must be between 5 and 200 characters");

            RuleFor(dto => dto.CategoryIcon)
                .NotEmpty().WithMessage("Icon cannot be empty")
                .Length(1, 50).WithMessage("Icon path must be between 10 and 50 characters");

        }
    }
}