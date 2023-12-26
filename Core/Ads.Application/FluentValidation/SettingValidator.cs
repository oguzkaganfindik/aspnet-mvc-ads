using Ads.Application.DTOs.Setting;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
    public class SettingDtoValidator : AbstractValidator<SettingDto>
    {
        public SettingDtoValidator()
        {
            RuleFor(dto => dto.Key)
                .NotEmpty().WithMessage("Key cannot be empty")
                .Length(2, 200).WithMessage("Key must be between 2 and 200 characters");

            RuleFor(dto => dto.Value)
                .NotEmpty().WithMessage("Value cannot be empty")
                .Length(2, 400).WithMessage("Value must be between 2 and 400 characters");
        }
    }
}