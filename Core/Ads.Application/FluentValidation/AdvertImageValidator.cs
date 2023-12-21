using Ads.Application.DTOs.AdvertImage;
using FluentValidation;

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
                .NotNull().WithMessage("Advert ID cannot be null")
                .GreaterThan(0).WithMessage("A valid advert ID is required");
        }
    }
}
