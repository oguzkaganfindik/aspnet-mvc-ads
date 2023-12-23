using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertImage;

namespace Ads.Web.Mvc.Areas.Admin.Models
{
    public class AdvertViewModel
    {
        public IEnumerable<AdvertDto> Adverts { get; set; }
        public IEnumerable<AdvertImageDto> AdvertImages { get; set; }
        public string AdvertImagePath { get; set; }
        public int AdvertId { get; set; }

    }
}
