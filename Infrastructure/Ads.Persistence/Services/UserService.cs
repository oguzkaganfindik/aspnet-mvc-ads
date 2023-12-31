//using Ads.Application.DTOs.User;
//using Ads.Application.Services;
//using Ads.Persistence.Contexts;
//using Ads.Persistence.Repositories;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;

//namespace Ads.Persistence.Services
//{
//    public class UserService : UserRepository, IUserService
//    {
//        private readonly IMapper _mapper;
//        public UserService(AppDbContext context, IMapper mapper) : base(context)
//        {
//            _mapper = mapper;
//        }

//        public async Task<List<UserDto>> GetAllUsersWithRelations()
//        {
//            var users = await _context.Users
//                                      .Include(x => x.Role)
//                                      .Include(u => u.Adverts)
//                                      .Include(u => u.Setting)
//                                      .Include(u => u.AdvertComments)
//                                      .Include(u => u.AdvertRatings)
//                                            .ThenInclude(ar => ar.Advert)
//                                      .ToListAsync();
//            return _mapper.Map<List<UserDto>>(users);


//        }

//    }
//}
