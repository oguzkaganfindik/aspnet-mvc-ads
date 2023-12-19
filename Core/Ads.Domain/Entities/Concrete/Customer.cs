using Ads.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Advert")]
        public int AdvertId { get; set; }
        [StringLength(50)]
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Display(Name = "Soyadı"), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string LastName { get; set; }
        [StringLength(11)]
        [Display(Name = "TC Numarası")]
        public string? TcNo { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Email { get; set; }
        [StringLength(500)]
        public string? Adress { get; set; }
        [StringLength(15)]
        public string? Phone { get; set; }
        public string? Notes { get; set; }

        [Display(Name = "Advert")]
        public virtual Advert? Advert { get; set; }
    }
}
