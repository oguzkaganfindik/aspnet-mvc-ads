using Ads.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
    public  class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email boş olamaz.").EmailAddress().WithMessage("Email uygun formatta değil!");
            RuleFor(x => x.Password).Matches(x => x.PasswordConfirm).WithMessage("Şifreler eşleşmemektedir.");
        }
    }
    
    
}
