using Ads.Application.DTOs.AdvertImage;
using Ads.Application.Repositories;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;

namespace Ads.Persistence.Services
{
    public class AdvertImageService : AdvertImageRepository, IAdvertImageService
    {
        private readonly IMapper _mapper;
        private readonly IAdvertImageRepository _repository; 

        public AdvertImageService(AppDbContext context, IMapper mapper, IAdvertImageRepository repository)
            : base(context)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public async Task AddAdvertImageAsync(AdvertImageDto advertImageDto)
        {
            var imagePath = await FileHelper.FileLoaderAsync(advertImageDto.File, "Img/AdvertImages/");
            advertImageDto.AdvertImagePath = imagePath;

            var advertImage = _mapper.Map<AdvertImage>(advertImageDto);

            await _repository.AddAsync(advertImage);
            await _repository.SaveAsync(); 
        }

        public async Task<int?> DeleteAdvertImageAsync(int advertImageId)
        {
            var advertImage = await _repository.GetByIdAsync(advertImageId);
            if (advertImage != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", advertImage.AdvertImagePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var advertId = advertImage.AdvertId;
                if (advertId == null)
                {
                    throw new InvalidOperationException("AdvertImage does not have an associated AdvertId.");
                }

                _repository.Delete(advertImage);
                await _repository.SaveAsync();

                return advertId.Value;
            }
            else
            {
                throw new ArgumentException("Resim bulunamadı.", nameof(advertImageId));
            }
        }
    }
}
