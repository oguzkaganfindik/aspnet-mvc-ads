using Ads.Application.DTOs.Advert;
using Ads.Application.Repositories;
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

        public async Task<List<AdvertDto>> GetAllAdvertsAsync()
        {
            var adverts = await _context.Adverts
                                        .Include(a => a.CategoryAdverts)
                                            .ThenInclude(ca => ca.Category)
                                        .Include(a => a.SubCategoryAdverts)
                                            .ThenInclude(ca => ca.SubCategory)
                                        .Include(a => a.AdvertImages)
                                        //.Include(a => a.AdvertComments)
                                        //.Include(a => a.AdvertRatings)
                                        .Include(a => a.User)
                                        // Diğer ilişkili alanlar gerekiyorsa, benzer şekilde Include edilebilir.
                                        .ToListAsync();
            return _mapper.Map<List<AdvertDto>>(adverts);
        }


        public async Task<AdvertDto> GetAdvertByIdAsync(int advertId)
        {
            var advert = await _context.Adverts
                                       .FirstOrDefaultAsync(a => a.Id == advertId);
            return advert != null ? _mapper.Map<AdvertDto>(advert) : null;
        }

        public async Task<AdvertDto> GetAdvertDetailsAsync(int advertId)
        {
            var advert = await _context.Adverts
                                       .Include(a => a.CategoryAdverts)
                                                   .ThenInclude(ca => ca.Category)
                                       .Include(a => a.SubCategoryAdverts)
                                                   .ThenInclude(ca => ca.SubCategory)
                                       .Include(a => a.AdvertImages)
                                       .Include(a => a.AdvertComments)
                                       .Include(a => a.AdvertRatings)
                                       .Include(a => a.User)
                                       .FirstOrDefaultAsync(a => a.Id == advertId);
            return advert != null ? _mapper.Map<AdvertDto>(advert) : null;
        }

        public async Task AddAdvertAsync(AdvertDto advertDto, List<int> selectedCategoryIds, List<int> selectedSubCategoryIds)
        {
            var advert = _mapper.Map<Advert>(advertDto);
            advert.CategoryAdverts = selectedCategoryIds.Select(id => new CategoryAdvert { CategoryId = id }).ToList();
            advert.SubCategoryAdverts = selectedSubCategoryIds.Select(id => new SubCategoryAdvert { SubCategoryId = id }).ToList();

            // Diğer işlemler...

            _context.Adverts.Add(advert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var advert = await _context.Adverts.FindAsync(id);
            if (advert != null)
            {
                _context.Adverts.Remove(advert);
                await _context.SaveChangesAsync();
            }
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