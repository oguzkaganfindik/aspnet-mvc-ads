using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.Page;
using Ads.Application.Services;
using Ads.Application.ViewModels;
using Ads.Persistence.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.Services
{
    public class NavbarService : INavbarService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NavbarService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NavbarViewModel> GetNavbarViewModelAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            var pages = await _context.Pages.Where(p => p.SettingId == 3).ToListAsync();


            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            var pageDtos = _mapper.Map<IEnumerable<PageDto>>(pages);

            var viewModel = new NavbarViewModel
            {
                Categories = categoryDtos,
                Pages = pageDtos
            };

            return viewModel;
        }
    }
}