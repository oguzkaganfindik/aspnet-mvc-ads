using Ads.Application.DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Length(5, 200).WithMessage("Description must be between 5 and 200 characters");

            RuleFor(dto => dto.IconPath)
                .NotEmpty().WithMessage("Icon path cannot be empty")
                .Length(10, 50).WithMessage("Icon path must be between 10 and 50 characters");

        }
    }
}