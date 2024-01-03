using Ads.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı boş olamaz")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı")
            .MaximumLength(20).WithMessage("Şifre en fazla 20 karakter  olabilir")
            .Equal(x => x.PasswordConfirm).WithMessage("Şifreler eşleşmiyor");

            RuleFor(x => x.PasswordConfirm)
                .NotEmpty().WithMessage("Şifre onay alanı boş olamaz");

        }
    }
}
