using Ads.Application.DTOs.User;
using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Ads.Application.Services
{
    public interface IUserService 
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<TDto> GetUserByIdAsync<TDto>(int id) where TDto : BaseUserDto;
        Task<IdentityResult> CreateUserAsync(UserDto userDto, IFormFile? userImageFile);
        Task<IdentityResult> UpdateUserAsync(UserEditDto userDto , IFormFile? userImageFile);
    }
}