using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Domain.Entities.Concrete
{
    public class Page : BaseEntity
    {

        [DisplayName("Page Name")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Name { get; set; }

        [DisplayName("Title 1")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Title1 { get; set; }

        [DisplayName("Title 2")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string? Title2 { get; set; }

        [DisplayName("Content 1")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(3000, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string Content1 { get; set; }

        [DisplayName("Is it Active?")]
        public bool IsActive { get; set; }

        [DisplayName("Content 2")]
        [StringLength(3000, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string? Content2 { get; set; }

        [DisplayName("Image Path")]
		[StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
		[MinLength(1, ErrorMessage = "{0} en az {1} karakter olabilir!")]
		public string? PageImagePath { get; set; }

        
        [ForeignKey("Setting")]
        public int? SettingId { get; set; }
        public virtual Setting? Setting { get; set; }
    }

}
