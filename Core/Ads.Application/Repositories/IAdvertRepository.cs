using Ads.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Repositories
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<List<Advert>> GetCustomAdvertList();
        Task<List<Advert>> GetCustomAdvertList(Expression<Func<Advert, bool>> expression);
        Task<Advert> GetCustomAdvert(int id);
    }
}
