using Ads.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Şifre alanı boş olamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
             .Matches("[A-Z]").WithMessage("En az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("En az bir küçük harf içermelidir.")
               .Matches("[0-9]").WithMessage("En az bir rakam içermelidir.")
               .Matches("[^a-zA-Z0-9]").WithMessage("En az bir özel karakter içermelidir.");

        }
    }
}
