using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
	public class SubCategory : BaseEntity
	{

        [DisplayName("Name")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Name { get; set; }

		public virtual Category Category { get;}

		[ForeignKey("Category")]
		public Guid CategoryId { get; set; }

		public virtual ICollection<SubCategoryAdvert> SubCategoryAdverts { get; set; }


	}
}
