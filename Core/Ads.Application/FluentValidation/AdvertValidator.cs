using Ads.Application.DTOs.Advert;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
	public class AdvertDtoValidator : AbstractValidator<AdvertDto>
    {
        public AdvertDtoValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .Length(3, 200).WithMessage("Title must be between 3 and 200 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");

            RuleFor(dto => dto.Price)
                .GreaterThanOrEqualTo(0).WithMessage("The price must be 0 or greater.");

            RuleFor(dto => dto.UserId)
                .GreaterThan(0).WithMessage("A valid user ID is required.");

            RuleFor(dto => dto.ClickCount)
              .GreaterThanOrEqualTo(0).WithMessage("The number of clicks must be 0 or greater.");

            RuleFor(dto => dto.CategoryAdverts)
                 .NotEmpty().WithMessage("At least one category must be selected.");

            RuleFor(dto => dto.SubCategoryAdverts)
                .NotEmpty().WithMessage("At least one subcategory must be selected.");

            RuleFor(dto => dto.AdvertImages)
             .Must(dto => dto != null && dto.Any())
             .WithMessage("At least one image must be added.");

            RuleFor(dto => dto.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");

            RuleFor(dto => dto.UpdatedDate)
                .Must((dto, updatedDate) => updatedDate == null || updatedDate >= dto.CreatedDate)
                .WithMessage("Update date cannot be earlier than creation date.");

            RuleFor(dto => dto.DeletedDate)
                .Must((dto, deletedDate) => deletedDate == null || (deletedDate >= dto.CreatedDate && (dto.UpdatedDate == null || deletedDate >= dto.UpdatedDate)))
                .WithMessage("Delete date cannot be earlier than creation and update dates.");


        }
    }
}