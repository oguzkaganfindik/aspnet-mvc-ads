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

        public SearchViewComponent(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string query)
        {
            var searchResultDtos = await _advertService.Search(query, 1, 5);
            return View(searchResultDtos);
        }

    }
}