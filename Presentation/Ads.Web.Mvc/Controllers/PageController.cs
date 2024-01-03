using Ads.Application.DTOs.AdvertImage;
using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence;
using Ads.Persistence.Services;
using Ads.Web.Mvc.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ads.Web.Mvc.Controllers
{
  public class PageController : Controller
  {
    private readonly IService<Page> _pageService;
    private readonly IMapper _mapper;

   

    public async Task<IActionResult> DetailAsync(int id)
    {
      try
      {
        LoggerHelper.LogInformation("DetailAsync işlemi başlatıldı", id);

        var page = await _pageService.FindAsync(id);
        if (page == null)
        {
          LoggerHelper.LogError(null, "Sayfa bulunamadı", id);
          return NotFound();
        }

        var model = _mapper.Map<PageDto>(page);
        LoggerHelper.LogInformation("DetailAsync işlemi başarıyla tamamlandı", id);
        return View(new List<PageDto> { model });
      }
      catch (Exception ex)
      {
        LoggerHelper.LogError(ex, "DetailAsync işlemi sırasında hata oluştu", id);
        throw;
      }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
