using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class AdvertRating : BaseEntity, IAuiditEntity
    {
        [Key, Column(Order = 0)]
        public Guid UserId { get; set; }

        [Key, Column(Order = 1)]
        public Guid AdvertId { get; set; } 

        public int? Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual Advert Advert { get; set; }

        public virtual User User { get; set; }
    }
}

