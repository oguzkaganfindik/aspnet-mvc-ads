using Ads.Application.DTOs.Advert;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ads.Application.DTOs.AdvertImage
{
    public class AdvertImageDto
    {
        public int Id { get; set; }

        public string? AdvertImagePath { get; set; }

        public IFormFile File { get; set; }

        public int? AdvertId { get; set; }
        public AdvertDto? Advert { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
