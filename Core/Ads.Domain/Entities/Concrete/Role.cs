using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class Role : IEntity, IAuiditEntity
    {
        public int Id { get; set; }

        [DisplayName("Name")]
		[Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Name { get; set; }

		public virtual ICollection<User> Users { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
