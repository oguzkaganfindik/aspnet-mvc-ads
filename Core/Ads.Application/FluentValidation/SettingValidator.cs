using Ads.Application.DTOs.Setting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class SettingDtoValidator : AbstractValidator<SettingDto>
    {
        public SettingDtoValidator()
        {
            RuleFor(dto => dto.Theme)
                .NotEmpty().WithMessage("Theme cannot be empty")
                .Length(5, 200).WithMessage("Theme must be between 5 and 200 characters");

            RuleFor(dto => dto.Value)
                .NotEmpty().WithMessage("Value cannot be empty")
                .Length(5, 400).WithMessage("Value must be between 5 and 400 characters");
        }
    }
}