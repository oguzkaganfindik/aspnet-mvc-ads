using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class AdvertRating : IEntity, IAuiditEntity
    {
        public int Id { get; set; }

        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int AdvertId { get; set; }

        [DisplayName("Rating")]
        [Range(1, 5, ErrorMessage = "{0} en az {1} en fazla {2} olabilir!")]
        public int? Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual Advert? Advert { get; set; }

        public virtual User? User { get; set; }
    }
}

