using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.Services
{
    public class AdvertService : AdvertRepository, IAdvertService
    {
        public AdvertService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Advert>> Search(string query, int page, int pageSize)
        {
            return await _context.Adverts
        .Include(a => a.AdvertImages) // AdvertImages koleksiyonunu dahil et
        .Include(a => a.CategoryAdverts) // CategoryAdverts koleksiyonunu dahil et
            .ThenInclude(ca => ca.Category) // Her CategoryAdvert için Category'yi dahil et
        .Where(a => a.Title.Contains(query) || a.Description.Contains(query))
        .OrderBy(a => a.Title)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
        }
    }
}