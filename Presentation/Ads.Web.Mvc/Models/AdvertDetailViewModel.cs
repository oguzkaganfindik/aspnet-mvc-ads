using Ads.Domain.Entities.Concrete;

namespace Ads.Web.Mvc.Models
{
    public class AdvertDetailViewModel
    {
        public Advert Advert { get; set; }
        public Customer? Customer { get; set; }
    }
}
