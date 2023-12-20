using Ads.Application.DTOs.AdvertImage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class AdvertImageDtoValidator : AbstractValidator<AdvertImageDto>
    {
        public AdvertImageDtoValidator()
        {
            RuleFor(dto => dto.AdvertImagePath)
                .NotEmpty().WithMessage("Image path cannot be empty")
                .Length(1, 200).WithMessage("Image path must be between 1 and 200 characters");

            RuleFor(dto => dto.AdvertId)
                .GreaterThan(0).WithMessage("A valid advert ID is required");
        }
    }
}
