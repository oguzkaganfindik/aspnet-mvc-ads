using Ads.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
	public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
	{
		public ForgotPasswordDtoValidator()
		{
			RuleFor(x => x.UserId)
				.GreaterThan(0).WithMessage("Invalid user ID.");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.")
				.Length(5, 100).WithMessage("Email must be between 5 and 100 characters.");
		}
	}
}
