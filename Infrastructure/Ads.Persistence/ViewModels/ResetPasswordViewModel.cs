using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre:")]
        public string? Password { get; set; }


        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Girmiş olduğunuz şifreler eşleşmemektedir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre Tekrar:")]
        public string? PasswordConfirm { get; set; }
    }
}
