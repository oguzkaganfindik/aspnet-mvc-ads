using Ads.Application.DTOs.SubCategory;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class SubCategoryDtoValidator : AbstractValidator<SubCategoryDto>
    {
        public SubCategoryDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters");

            RuleFor(dto => dto.CategoryId)
					.NotEmpty().WithMessage("Category ID is required.")
			.GreaterThan(0).WithMessage("Category ID must be greater than zero.");

		}
    }
}
