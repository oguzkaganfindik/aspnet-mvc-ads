using Ads.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public class ChangeMailValidator:AbstractValidator<ChangeMailDto>
    {
        public ChangeMailValidator()
        {
            RuleFor(x => x.NewEmail)
      .NotEmpty().WithMessage("Yeni e-posta adresi boş olamaz")
      .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin");

            RuleFor(x => x.NewEmail)
                .NotEmpty().WithMessage("Yeni e-posta onay alanı boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin")
                .Equal(x => x.NewEmail).WithMessage("E-posta adresleri eşleşmiyor");

        }
    }
}
