using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class User : BaseEntity, IAuiditEntity
    {

        [DisplayName("Profile Picture")]
		//[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(10, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string ImagePath { get; set; }

		[DisplayName("Email")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Password { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string FirstName { get; set; }

		[DisplayName("Last Name")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string LastName { get; set; }

		[DisplayName("Username")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Username { get; set; }

		[DisplayName("Adress")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Address { get; set; }

        [DisplayName("Phone Number")]
        [StringLength(50, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "{0} sadece rakamlardan oluşmalıdır.")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        public string Phone { get; set; }

		[DisplayName("Is it Active?")]
		public bool IsActive { get; set; }

		[DisplayName("Is it Active?")]
		public string IsActiveString => IsActive ? "Active" : "Passive";

		public virtual ICollection<Advert> Adverts { get; set; }

        public virtual ICollection<AdvertComment> AdvertComments { get; set; }

		public virtual ICollection<AdvertRating> AdvertRatings { get; set; }

		public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual Role Role { get; set; }
		       
		public Guid RoleId { get; set; }

		public virtual Setting Setting { get; set; }
        
        public Guid SettingId { get; set; }
	}
}
