using Ads.Application.Services;
using Ads.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Ads.Web.Mvc.ViewComponents
{
    public class AdvertStarViewComponent : ViewComponent
    {
        private readonly IAdvertService _service;


        public AdvertStarViewComponent(IAdvertService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int advertId)
        {
            var ratings = await _service.GetAdvertRatings(advertId);


            int fiveStarRatingsCount = ratings.Count(r => r.Rating == 5);
            int fourStarRatingsCount = ratings.Count(r => r.Rating == 4);
            int threeStarRatingsCount = ratings.Count(r => r.Rating == 3);
            int twoStarRatingsCount = ratings.Count(r => r.Rating == 2);
            int oneStarRatingsCount = ratings.Count(r => r.Rating == 1);

            double fiveRatioPercent = ratings.Count() > 0 ? Math.Round((double)fiveStarRatingsCount / ratings.Count() * 100, 2) : 0;
            double fourRatioPercent = ratings.Count() > 0 ? Math.Round((double)fourStarRatingsCount / ratings.Count() * 100, 2) : 0;
            double threeRatioPercent = ratings.Count() > 0 ? Math.Round((double)threeStarRatingsCount / ratings.Count() * 100, 2) : 0;
            double twoRatioPercent = ratings.Count() > 0 ? Math.Round((double)twoStarRatingsCount / ratings.Count() * 100, 2) : 0;
            double oneRatioPercent = ratings.Count() > 0 ? Math.Round((double)oneStarRatingsCount / ratings.Count() * 100, 2) : 0;


            var totalPoint = (fiveStarRatingsCount * 5) + (fourStarRatingsCount * 4) + (threeStarRatingsCount * 3) + (twoStarRatingsCount * 2) + oneStarRatingsCount;
            var totalRating = ratings.Count();
            var averageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
            var averageRatingValue = averageRating.HasValue ? averageRating.Value : 0.0;

            var model = new PopularAdvertsViewModel
            {
                TotalPoint = totalPoint,
                TotalRating = totalRating,
                AverageRating = averageRatingValue,
                FiveRatioPercent = fiveRatioPercent,
                FourRatioPercent = fourRatioPercent,
                ThreeRatioPercent = threeRatioPercent,
                TwoRatioPercent = twoRatioPercent,
                OneRatioPercent = oneRatioPercent,
                FiveStarRatingsCount = fiveStarRatingsCount,
                FourStarRatingsCount = fourStarRatingsCount,
                ThreeStarRatingsCount = threeStarRatingsCount,
                TwoStarRatingsCount = twoStarRatingsCount,
                OneStarRatingsCount = oneStarRatingsCount,
            };

            return View(model);
        }
    }
}
