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
    public class AdvertCommentService : AdvertCommentRepository, IAdvertCommentService
    {
        public AdvertCommentService(AppDbContext context) : base(context)
        {
        }
    }
}
