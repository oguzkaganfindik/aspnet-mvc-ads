using Ads.Domain.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IAuiditEntity
    {
        [DisplayName("Profile Picture")]
        //[Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string? UserImagePath { get; set; }

        //[DisplayName("Email")]
        //[Required(ErrorMessage = "{0} boş geçilemez.")]
        //[StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        //[MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        //public string Email { get; set; }


        [DisplayName("First Name")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string LastName { get; set; }


        [DisplayName("Address")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string? Address { get; set; }

        [DisplayName("Phone Number")]
        [StringLength(50, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string? Phone { get; set; }

        [DisplayName("Is it Active?")]
        public bool IsActive { get; set; }

        [DisplayName("Is it Active?")]
        public string IsActiveString => IsActive ? "Active" : "Passive";

        public DateTime? BirthDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }



        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        [ForeignKey("Setting")]
        public int? SettingId { get; set; }

        // Navigation Properties
        public virtual AppRole? Role { get; set; }
        public virtual Setting? Setting { get; set; }
        public virtual ICollection<Advert>? Adverts { get; set; }
        public virtual ICollection<AdvertComment>? AdvertComments { get; set; }
        public virtual ICollection<AdvertRating>? AdvertRatings { get; set; }
    }
}