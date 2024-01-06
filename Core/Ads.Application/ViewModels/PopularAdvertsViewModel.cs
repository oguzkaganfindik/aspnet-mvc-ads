namespace Ads.Application.ViewModels
{
	public class PopularAdvertsViewModel
	{
		public int TotalPoint { get; set; }
		public int TotalRating { get; set; }
		public double AverageRating { get; set; }
		public double FiveRatioPercent { get; set; }
		public double FourRatioPercent { get; set; }
		public double ThreeRatioPercent { get; set; }
		public double TwoRatioPercent { get; set; }
		public double OneRatioPercent { get; set; }
		public int FiveStarRatingsCount { get; set; }
		public int FourStarRatingsCount { get; set; }
		public int ThreeStarRatingsCount { get; set; }
		public int TwoStarRatingsCount { get; set; }
		public int OneStarRatingsCount { get; set; }
	}
}
