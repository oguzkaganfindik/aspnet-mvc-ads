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
        private readonly IMapper _mapper;

        public AdvertController(IAdvertService advertService, IMapper mapper)
        {
            _advertService = advertService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Search(string query, int page = 1, int pageSize = 5)
        {
            var adverts = await _advertService.Search(query, page, pageSize);
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
