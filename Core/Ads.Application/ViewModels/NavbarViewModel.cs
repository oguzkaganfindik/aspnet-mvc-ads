using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.Page;

namespace Ads.Application.ViewModels
{
    public class NavbarViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<PageDto> Pages { get; set; }
    }
}
