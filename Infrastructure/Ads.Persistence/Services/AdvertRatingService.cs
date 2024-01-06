using Ads.Application.DTOs.AdvertImage;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.Repositories;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;

namespace Ads.Persistence.Services
{
    public class AdvertRatingService : AdvertRatingRepository, IAdvertRatingService
    {
        private readonly IMapper _mapper;
        private readonly IAdvertRatingRepository _repository;
        public AdvertRatingService(AppDbContext context, IMapper mapper, IAdvertRatingRepository repository) : base(context)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAdvertRatingAsync(AdvertRatingDto advertRatingDto)
        {
           
            var advertRating = _mapper.Map<AdvertRating>(advertRatingDto);

            await _repository.AddAsync(advertRating);
            await _repository.SaveAsync();
        }
        public async Task<int?> DeleteAdvertRatingAsync(int userId, int advertId)
        {
            //Buraya advertid den userıd buacak olan method?
            
            var advertRating = await _repository.GetByUserIdAndAdvertIdAsync(userId, advertId);
            if (advertRating != null)
            {
                int currentAdvertId = advertRating.AdvertId;
                if (currentAdvertId == 0)
                {
                    throw new InvalidOperationException("AdvertRating does not have an associated AdvertId.");
                }

                _repository.Delete(advertRating);
                await _repository.SaveAsync();

                return currentAdvertId;
            }
            else
            {
                throw new ArgumentException("Rating not found.", nameof(userId));
            }
        }


    }
}
