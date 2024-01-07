using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.Repositories;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using Ads.Persistence.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.Services
{
    public class AdvertCommentService : AdvertCommentRepository, IAdvertCommentService
    {
        private readonly IMapper _mapper;
        private readonly IAdvertCommentRepository _repository;
        public AdvertCommentService(AppDbContext context, IMapper mapper, IAdvertCommentRepository repository) : base(context)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAdvertCommentAsync(AdvertCommentDto advertCommentDto)
        {

            var advertComment = _mapper.Map<AdvertComment>(advertCommentDto);

            await _repository.AddAsync(advertComment);
            await _repository.SaveAsync();
        }


    }
}
