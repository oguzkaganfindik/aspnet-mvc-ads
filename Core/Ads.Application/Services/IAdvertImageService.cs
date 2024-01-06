using Ads.Application.DTOs.AdvertImage;
using Ads.Application.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Services
{
    public interface IAdvertImageService : IAdvertImageRepository
    {
        Task AddAdvertImageAsync(AdvertImageDto advertImageDto);
        Task<int?> DeleteAdvertImageAsync(int advertImageId);
    }
}
