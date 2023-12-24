using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class SubCategory : IEntity, IAuiditEntity
    {
        public int Id { get; set; }

        [Display(Name = "SubCategory Name")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
		[StringLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string Name { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey("Category")]
		public int CategoryId { get; set; }

		public virtual ICollection<SubCategoryAdvert>? SubCategoryAdverts { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
