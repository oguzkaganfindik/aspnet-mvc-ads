using Ads.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class AdvertRating : IEntity, IAuiditEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AdvertId { get; set; } 

        public int? Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual Advert? Advert { get; set; }

        public virtual User? User { get; set; }
    }
}

