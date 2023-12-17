using Ads.Application.Services;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.Services
{
    public class AdvertImageService : AdvertImageRepository, IAdvertImageService
    {
        public AdvertImageService(AppDbContext context) : base(context)
        {
        }
    }
}
