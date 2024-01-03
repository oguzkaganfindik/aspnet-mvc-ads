using Microsoft.AspNetCore.Identity;

namespace Ads.Application.Services
{
    public interface IMemberService
    {
        Task LogOutAsync();
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<(bool, IEnumerable<IdentityError>)> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
        Task<bool> CheckPasswordAsync(object userName, string? passwordOld);
        Task<(bool isSuccess, object errors)> ChangePasswordAsync(object userName, object passwordNew, string passwordOld);
    }
}

