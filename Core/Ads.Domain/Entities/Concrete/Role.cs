using Ads.Domain.Entities.Abstract;
using Ads.Domain.Entities.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
	public class Role : BaseEntity
	{

        [DisplayName("Name")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Name { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}
