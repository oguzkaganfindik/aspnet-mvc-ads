using Ads.Application.DTOs.AdvertImage;
using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Services;
using Ads.Web.Mvc.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ads.Web.Mvc.Controllers
{
    public class PageController : Controller
    {
        private readonly IService<Page> _pageService;
        private readonly IMapper _mapper;

        public PageController(IService<Page> service, IMapper mapper)
        {
            _pageService = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> DetailAsync(int id)
        {
            var page = await _pageService.FindAsync(id);
            var model = _mapper.Map<PageDto>(page);
            return View(new List<PageDto> { model });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
