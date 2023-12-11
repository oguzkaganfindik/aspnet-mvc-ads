using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class CategoryAdvert : BaseEntity
    {

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Advert Advert { get; set; }

        [ForeignKey("Advert")]
        public int AdvertId { get; set; }

        public virtual Category Category { get; set; }
    }
}