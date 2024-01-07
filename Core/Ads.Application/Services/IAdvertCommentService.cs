using Ads.Application.DTOs.AdvertComment;
using Ads.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.Services
{
    public interface IAdvertCommentService : IAdvertCommentRepository
    {
        Task AddAdvertCommentAsync(AdvertCommentDto advertCommentDto);
    }
}
