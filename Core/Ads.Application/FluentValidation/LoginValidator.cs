using Ads.Application.DTOs.User;
using FluentValidation;

namespace Ads.Application.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
	{
		public LoginDtoValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Username is required.")
				.Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required.")
				.Length(6, 50).WithMessage("Password must be between 6 and 50 characters.");
		}
	}
}
