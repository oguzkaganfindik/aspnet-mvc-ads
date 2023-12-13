using Ads.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class SubCategoryAdvert : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Advert Advert { get; set; }

        [ForeignKey("Advert")]
        public int AdvertId { get; set; }
    }
}