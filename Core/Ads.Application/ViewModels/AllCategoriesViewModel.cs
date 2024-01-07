using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.SubCategory;
using Ads.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.ViewModels
{
    public class AllCategoriesViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<SubCategoryDto> SubCategories { get; set; }
    }
}
