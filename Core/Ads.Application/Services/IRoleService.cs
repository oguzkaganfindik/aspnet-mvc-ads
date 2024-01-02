using Ads.Application.DTOs.Role;

namespace Ads.Application.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleWithUsersAsync(int roleId);
    }
}