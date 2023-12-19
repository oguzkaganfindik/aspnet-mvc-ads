using Ads.Domain.Entities.Concrete;

namespace Ads.Web.Mvc.Models
{
    public class HomePageViewModel
    {
        public List<Page> Pages { get; set; }
        public List<Advert> Adverts { get; set; }
        public List<Category> Categories { get; set; }
        public List<CategoryAdvert> CategoryAdverts { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<SubCategoryAdvert> SubCategoryAdverts { get; set; }

    }
}
