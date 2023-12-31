using Ads.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Ads.Web.Mvc.CustomValidations
{
    public class UserValidator :IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isNumeric = int.TryParse(user.UserName[0]!.ToString(), out _);

            if (isNumeric)
            {
                errors.Add(new() { Code = "UserNameContainFirstLetterDigit", Description = "Kullanıcı adının ilk karekteri sayısal bir karekter içeremez." });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed());
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
