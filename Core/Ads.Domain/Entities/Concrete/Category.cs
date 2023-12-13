using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        [DisplayName("Name")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Name { get; set; }

		[DisplayName("Description")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Description { get; set; }

		[DisplayName("Icon")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(50, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(10, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string IconPath { get; set; }

		public virtual ICollection<CategoryAdvert> CategoryAdverts { get; set; }
		public virtual ICollection<SubCategory> SubCategories { get; set; }
	
	}
}