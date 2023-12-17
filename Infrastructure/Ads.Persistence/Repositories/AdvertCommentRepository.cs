using Ads.Application.Repositories;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.Repositories
{
    public class AdvertCommentRepository : Repository<AdvertComment>, IAdvertCommentRepository
    {
        public AdvertCommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<AdvertComment> GetCustomAdvertComment(int id)
        {
            return await _dbSet
              .Include(x => x.Advert)
              .Include(x => x.User)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<AdvertComment>> GetCustomAdvertCommentList()
        {
            return await _dbSet
                .Include(x => x.Advert)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<List<AdvertComment>> GetCustomAdvertCommentList(Expression<Func<AdvertComment, bool>> expression)
        {
            return await _dbSet
               .Where(expression)
               .Include(x => x.Advert)
               .Include(x => x.User)
               .ToListAsync();
        }


    }
}
