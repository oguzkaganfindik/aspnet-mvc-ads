using Ads.Application.DTOs.AdvertRating;
using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Services
{
    public interface IAdvertRatingService : IAdvertRatingRepository
    {
        Task AddAdvertRatingAsync(AdvertRatingDto advertRatingDto);
        Task<int?> DeleteAdvertRatingAsync(int userId, int advertId);
    }
}
