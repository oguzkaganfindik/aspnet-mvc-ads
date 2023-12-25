using Ads.Application.DTOs.Advert;
using Ads.Application.Services;
using Ads.Persistence.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Controllers
{
  public class AdvertController : Controller
  {
        private readonly IAdvertService _advertService;
        private const int PageSize = 5;
        private readonly IMapper _mapper;

        public AdvertController(IAdvertService advertService, IMapper mapper)
        {
            _advertService = advertService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Search(string query, int page = 1)
        {
            var adverts = await _advertService.Search(query, page, PageSize);
            var advertDtos = _mapper.Map<List<AdvertDto>>(adverts); 
            return View(advertDtos);
        }



        //public IActionResult Detail(int id)
        //{
        //	// Veritabanından ilanı bul
        //	var advert = _context.Adverts.FirstOrDefault(a => a.Id == id);
        //	if (advert == null)
        //	{
        //		return NotFound();
        //	}

        //	// İlan detaylarını görüntüle
        //	return View(advert);
        //}
    }
}
