using Ads.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Services
{
    public interface INavbarService
    {
        Task<NavbarViewModel> GetNavbarViewModelAsync();
    }
}
