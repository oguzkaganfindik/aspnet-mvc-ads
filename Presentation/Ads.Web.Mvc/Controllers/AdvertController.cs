using Ads.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.Mvc.Controllers
{
  public class AdvertController : Controller
  {
    private readonly AppDbContext _context;
    private const int PageSize = 5; // Sayfa başına gösterilecek kayıt sayısı

    public AdvertController(AppDbContext context)
    {
      _context = context;
    }

    public IActionResult Search(string query, int page = 1)
    {
			var searchResults = _context.Adverts
				 .Include(a => a.AdvertImages) //AdvertImages koleksiyonunu dahil et
				 .Include(a => a.CategoryAdverts) //CategoryAdverts koleksiyonunu dahil et
						 .ThenInclude(ca => ca.Category) //Her CategoryAdvert için Category'yi dahil et/Burası Düzelecek(sahip olunan kategori girilecek)
				 .Where(a => a.Title.Contains(query) || a.Description.Contains(query))
				 .OrderBy(a => a.Title)
				 .Skip((page - 1) * PageSize)
				 .Take(PageSize)
				 .ToList();

			return View(searchResults);


		}

		public IActionResult Detail(int id)
		{
			// Veritabanından ilanı bul
			var advert = _context.Adverts.FirstOrDefault(a => a.Id == id);
			if (advert == null)
			{
				return NotFound();
			}

			// İlan detaylarını görüntüle
			return View(advert);
		}
	}
}
