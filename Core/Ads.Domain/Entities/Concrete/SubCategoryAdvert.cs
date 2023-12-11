using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class SubCategoryAdvert : BaseEntity
    {

        [ForeignKey("SubCategory")]
        public Guid SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Advert Advert { get; set; }

        [ForeignKey("Advert")]
        public Guid AdvertId { get; set; }
    }
}