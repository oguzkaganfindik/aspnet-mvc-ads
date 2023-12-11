using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class AdvertComment : BaseEntity, IAuiditEntity
    {

        [DisplayName("Comment")]
        [StringLength(500, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(10, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string? Comment { get; set; }

        [DisplayName("Is it Active?")]
        public bool IsActive { get; set; }

        [DisplayName("Is it Active?")]
        public string IsActiveString => IsActive ? "Active" : "Passive";

        public virtual Advert Advert { get; set; }

        [ForeignKey("Advert")]
        public Guid AdvertId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
