using Ads.Application.DTOs.Page;
using Ads.Application.Repositories;

namespace Ads.Application.Services
{
    public interface IPageService : IPageRepository 
    {
        Task<List<PageDto>> GetAllPagesWithSettingsAsync();
        Task CreateAsync(PageDto pageDto, string pageImagePath, bool pageVisibility);
        Task UpdateAsync(PageDto pageDto, string pageImagePath, bool pageVisibility);
        Task<PageDto> GetPageByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}