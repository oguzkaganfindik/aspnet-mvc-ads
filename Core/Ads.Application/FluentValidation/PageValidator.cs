using Ads.Application.DTOs.Page;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class PageDtoValidator : AbstractValidator<PageDto>
    {
        public PageDtoValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("Title cannot be empty")
                .Length(5, 200).WithMessage("Title must be between 5 and 200 characters");

            RuleFor(dto => dto.Content)
                .NotEmpty().WithMessage("Content cannot be empty")
                .Length(5, 3000).WithMessage("Content must be between 5 and 3000 characters");

            RuleFor(dto => dto.ImagePath)
                .NotEmpty().WithMessage("Image path cannot be empty")
                .Length(10, 200).WithMessage("Image path must be between 10 and 200 characters");

            RuleFor(dto => dto.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future");

            RuleFor(dto => dto.UpdatedDate)
                .Must((page, updatedDate) => updatedDate == null || updatedDate >= page.CreatedDate)
                .WithMessage("Update date cannot be earlier than the creation date");

            RuleFor(dto => dto.DeletedDate)
                .Must((page, deletedDate) => deletedDate == null || (deletedDate >= page.CreatedDate && (page.UpdatedDate == null || deletedDate >= page.UpdatedDate)))
                .WithMessage("Delete date cannot be earlier than the creation and update dates");
        }
    }
}