using Ads.Application.DTOs.AdvertRating;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
	public class AdvertRatingDtoValidator :AbstractValidator<AdvertRatingDto>
    {
        public AdvertRatingDtoValidator()
        {
            RuleFor(dto => dto.UserId)
                .GreaterThan(0).WithMessage("A valid user ID is required");

            RuleFor(dto => dto.AdvertId)
                .GreaterThan(0).WithMessage("A valid advert ID is required");

            RuleFor(dto => dto.Rating)
                .InclusiveBetween(1, 5).When(rating => rating.Rating.HasValue)
                .WithMessage("Rating must be between 1 and 5");
         
            RuleFor(dto => dto.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future");

            RuleFor(dto => dto.UpdatedDate)
                .Must((dto, updatedDate) => updatedDate == null || updatedDate >= dto.CreatedDate)
                .WithMessage("Update date cannot be earlier than the creation date");

            RuleFor(dto => dto.DeletedDate)
                .Must((dto, deletedDate) => deletedDate == null || (deletedDate >= dto.CreatedDate && (dto.UpdatedDate == null || deletedDate >= dto.UpdatedDate)))
                .WithMessage("Delete date cannot be earlier than the creation and update dates");
        }
    }
}