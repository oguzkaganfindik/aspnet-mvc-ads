using Ads.Application.DTOs.User;

namespace Ads.Application.Services
{
    public interface IUserService 
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
    }
}