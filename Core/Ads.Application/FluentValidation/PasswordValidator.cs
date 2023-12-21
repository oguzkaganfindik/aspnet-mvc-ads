using Ads.Application.DTOs.User;
using FluentValidation;

namespace Ads.Application.FluentValidation
{

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
	{
		public ChangePasswordDtoValidator()
		{
			RuleFor(x => x.UserId)
				.GreaterThan(0).WithMessage("Invalid user ID.");

			RuleFor(x => x.OldPassword)
				.NotEmpty().WithMessage("Old password is required.")
				.Length(6, 50).WithMessage("Old password must be between 6 and 50 characters.");

			RuleFor(x => x.NewPassword)
				.NotEmpty().WithMessage("New password is required.")
				.Length(6, 50).WithMessage("New password must be between 6 and 50 characters.")
				.NotEqual(x => x.OldPassword).WithMessage("New password cannot be the same as the old password.");
		}
	}

}
