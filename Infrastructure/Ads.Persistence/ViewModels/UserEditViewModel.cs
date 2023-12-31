using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace Ads.Persistence.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Ad alanı boş bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz.")]
        [Display(Name = "Telefon Numarası: ")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi: ")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Şehir: ")]
        public string? City { get; set; }

        [Display(Name = "Profil Resmi: ")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Cinsiyet: ")]
        public Gender? Gender { get; set; }
    }
}
