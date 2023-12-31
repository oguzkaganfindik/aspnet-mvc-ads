using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class AdvertRating : BaseEntity
    {
        public int UserId { get; set; }

        public int AdvertId { get; set; }

        [DisplayName("Rating")]
        [Range(1, 5, ErrorMessage = "{0} en az {1} en fazla {2} olabilir!")]
        public int? Rating { get; set; }


        public virtual Advert? Advert { get; set; }

        public virtual AppUser? User { get; set; }
    }
}

