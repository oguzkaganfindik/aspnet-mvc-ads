using Ads.Application.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Ads.Application.Services
{
    public interface IUserService 
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IdentityResult> CreateUserAsync(UserDto userDto, IFormFile? userImageFile);
    }
}