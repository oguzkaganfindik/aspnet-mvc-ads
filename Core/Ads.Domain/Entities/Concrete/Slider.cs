using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class Slider : IEntity, IAuiditEntity
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Title { get; set; }

        [DisplayName("Content")]
        //[Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(3000, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Content { get; set; }

        [DisplayName("Image Path")]
        //[Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string ImagePath { get; set; }

        [DisplayName("Is it Active?")]
        public bool IsActive { get; set; }

        [DisplayName("Is it Active?")]
        public string IsActiveString => IsActive ? "Active" : "Passive";

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<Advert>? Adverts { get; set; }

        [StringLength(100)]
        public string? Link { get; set; }

    }

}
