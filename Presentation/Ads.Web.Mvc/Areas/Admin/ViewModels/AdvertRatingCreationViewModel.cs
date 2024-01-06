﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ads.Web.Mvc.Areas.Admin.ViewModels
{
    public class AdvertRatingCreationViewModel
    {

        public int AdvertId { get; set; }
        public int? UserId { get; set; }
        public List<SelectListItem>? Users { get; set; }
        public int Rating { get; set; }
    }
}
