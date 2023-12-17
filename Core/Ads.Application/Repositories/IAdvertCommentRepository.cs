using Ads.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Repositories
{
    public interface IAdvertCommentRepository : IRepository<AdvertComment>
    {
        Task<List<AdvertComment>> GetCustomAdvertCommentList();
        Task<List<AdvertComment>> GetCustomAdvertCommentList(Expression<Func<AdvertComment, bool>> expression);
        Task<AdvertComment> GetCustomAdvertComment(int id);
    }
}
