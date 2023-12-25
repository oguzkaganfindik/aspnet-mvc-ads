using Ads.Application.DTOs.Advert;
using Ads.Application.Services;
using Ads.Persistence.Services;
using Ads.Web.Mvc.ViewComponents;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly IAdvertService _advertService;
        private readonly IMapper _mapper;

        public SearchViewComponent(IAdvertService advertService, IMapper mapper)
        {
            _advertService = advertService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string query)
        {
            var searchResults = await _advertService.Search(query, 1, 5);
            var searchResultDtos = _mapper.Map<List<AdvertDto>>(searchResults);
            return View(searchResultDtos);
        }
    }
}