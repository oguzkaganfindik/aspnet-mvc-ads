using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class Setting : BaseEntity
    {

        [DisplayName("Key")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Key { get; set; }

        [DisplayName("Value")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(400, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Value { get; set; }

        // Navigation Properties

        public virtual ICollection<AppUser> Users { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}