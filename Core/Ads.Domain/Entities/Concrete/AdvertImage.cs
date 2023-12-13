using Ads.Domain.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class AdvertImage : IEntity
    {
        public int Id { get; set; }

        [DisplayName("Image Path")]
        //[Required(ErrorMessage = "{0} boş geçilemez.")]
        [StringLength(200, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(10, ErrorMessage = "{0} en az {1} karakter olabilir!")]
        public string ImagePath { get; set; }

        public virtual Advert Advert { get; set; }

		public int AdvertId { get; set; }
	}
}
