using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.ViewModels
{
    public class AdvertCommentCreationViewModel
    {
        public int AdvertId { get; set; }
        public int? UserId { get; set; }
        public List<SelectListItem>? Users { get; set; }
        public string Comment { get; set; }

        public bool IsActive { get; set; }
    }
}
