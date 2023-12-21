using Ads.Application.DTOs.Advert;
using System.ComponentModel.DataAnnotations;

namespace Ads.Application.DTOs.AdvertImage
{
    public class AdvertImageDto
	{
		public int Id { get; set; }
		public string AdvertImagePath { get; set; }

        [Required(ErrorMessage = "Advert ID is required.")]
        public int AdvertId { get; set; }
        public AdvertDto Advert { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
