using Ads.Application.DTOs.User;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator() 
        {
            RuleFor(dto => dto.Email)
               .NotEmpty().WithMessage("Email cannot be empty")
               .Length(5, 200).WithMessage("Email must be between 5 and 200 characters")
               .EmailAddress().WithMessage("Invalid email format");

            //RuleFor(dto => dto.Password)
            //    .NotEmpty().WithMessage("Password cannot be empty")
            //    .Length(5, 200).WithMessage("Password must be between 5 and 200 characters");

            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty")
                .Length(5, 100).WithMessage("First name must be between 5 and 100 characters");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty")
                .Length(5, 100).WithMessage("Last name must be between 5 and 100 characters");

            RuleFor(dto => dto.Username)
                .NotEmpty().WithMessage("Username cannot be empty")
                .Length(5, 100).WithMessage("Username must be between 5 and 100 characters");

            RuleFor(dto => dto.Address)
                .NotEmpty().WithMessage("Address cannot be empty")
                .Length(5, 200).WithMessage("Address must be between 5 and 200 characters");

            RuleFor(dto => dto.Phone)
                .NotEmpty().WithMessage("Phone number cannot be empty")
                .Length(1, 50).WithMessage("Phone number must be between 1 and 50 characters")
                .Matches("^[0-9]*$").WithMessage("Phone number must only contain digits");

            RuleFor(dto => dto.RoleId)
                .GreaterThan(0).WithMessage("A valid role ID is required");

            RuleFor(dto => dto.SettingId)
                .GreaterThan(0).WithMessage("A valid setting ID is required");
            
        }
    }
}
