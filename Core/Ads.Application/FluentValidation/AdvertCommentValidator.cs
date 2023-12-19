using Ads.Application.DTOs.AdvertComment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class AdvertCommentDtoValidator : AbstractValidator<AdvertCommentDto>
    {
        public AdvertCommentDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .NotEmpty().WithMessage("Comment cannot be empty")
                .Length(10, 500).WithMessage("Comment must be between 10 and 500 characters");

            RuleFor(dto => dto.AdvertId)
                .GreaterThan(0).WithMessage("A valid advert ID is required");

            RuleFor(dto => dto.UserId)
                .GreaterThan(0).WithMessage("A valid user ID is required");
            
            RuleFor(dto => dto.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future");

            RuleFor(dto => dto.UpdatedDate)
                .Must((dto, updatedDate) => updatedDate == null || updatedDate >=   dto.CreatedDate)
                .WithMessage("Update date cannot be earlier than the creation date");

            RuleFor(dto => dto.DeletedDate)
                .Must((dto, deletedDate) => deletedDate == null || (deletedDate >= dto.CreatedDate && (dto.UpdatedDate == null || deletedDate >= dto.UpdatedDate)))
                .WithMessage("Delete date cannot be earlier than the creation and update dates");
        }
    }
}

