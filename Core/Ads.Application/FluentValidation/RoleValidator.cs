using Ads.Application.DTOs.Role;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .Length(5, 100).WithMessage("Name must be between 5 and 100 characters");
        }
    }
}