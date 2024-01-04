using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ads.Persistence.Services
{
    public class PageService : PageRepository, IPageService
    {
        private readonly IMapper _mapper;


        public PageService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<PageDto>> GetAllPagesWithSettingsAsync()
        {
            var pages = await _context.Pages
                                      .Include(p => p.Setting)
                                      .ToListAsync();
            return _mapper.Map<List<PageDto>>(pages);
        }

        public async Task CreateAsync(PageDto pageDto, string pageImagePath, bool pageVisibility)
        {
            var newPage = _mapper.Map<Page>(pageDto);
            newPage.PageImagePath = pageImagePath;

            // PageVisibility'ye göre Setting ayarlaması yapabilirsiniz
            // Örnek:
            var setting = await _context.Settings
                                        .FirstOrDefaultAsync(s => s.Key == "Make the Page Visible" && s.Value == (pageVisibility ? "True" : "False"));

            if (setting != null)
            {
                newPage.SettingId = setting.Id;
            }

            _context.Pages.Add(newPage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PageDto pageDto, string pageImagePath, bool pageVisibility)
        {
            var pageToUpdate = await _context.Pages.FindAsync(pageDto.Id);
            if (pageToUpdate != null)
            {
                _mapper.Map(pageDto, pageToUpdate);
                pageToUpdate.PageImagePath = pageImagePath;

                // PageVisibility'ye göre Setting ayarlaması yapabilirsiniz
                var setting = await _context.Settings
                                            .FirstOrDefaultAsync(s => s.Key == "Make the Page Visible" && s.Value == (pageVisibility ? "True" : "False"));
                if (setting != null)
                {
                    pageToUpdate.SettingId = setting.Id;
                }

                _context.Pages.Update(pageToUpdate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PageDto> GetPageByIdAsync(int id)
        {
            var page = await _context.Pages
                                     .Include(p => p.Setting)
                                     .SingleOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return null;
            }

            return _mapper.Map<PageDto>(page);
        }

        public async Task DeleteAsync(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }
        }
    }
}