using Ads.Application.DTOs.Advert;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.Services
{
    public class AdvertService : AdvertRepository, IAdvertService
    {

        private readonly IMapper _mapper;
        public AdvertService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdvertDto>> Search(string query, int page, int pageSize)
        {
            var adverts = await _context.Adverts
                .Include(a => a.AdvertImages)
                .Include(a => a.CategoryAdverts)
                    .ThenInclude(ca => ca.Category)
                .Where(a => a.Title.Contains(query) || a.Description.Contains(query))
                .OrderBy(a => a.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AdvertDto>>(adverts);
        }
        public async Task<IEnumerable<AdvertDto>> GetTrendingAddsAsync()
        {
            var adverts = await _context.Adverts
                            .Include(a => a.AdvertImages)
                            .Include(a => a.CategoryAdverts)
                                .ThenInclude(ca => ca.Category)
                            .OrderByDescending(a => a.ClickCount)
                            .Take(5)
                            .ToListAsync();

            return _mapper.Map<IEnumerable<AdvertDto>>(adverts);
        }

    }
}