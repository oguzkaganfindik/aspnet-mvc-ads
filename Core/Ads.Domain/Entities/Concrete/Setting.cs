using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class Setting : IEntity, IAuiditEntity
    {
        public int Id { get; set; }

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

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // One-to-One ilişki için User referansı
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        // One-to-One ilişki için Page referansı
        public int? PageId { get; set; }
        public virtual Page Page { get; set; }

    }
}
