using Ads.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetCustomList();
        Task<List<User>> GetCustomList(Expression<Func<User, bool>> expression);
    }
}
