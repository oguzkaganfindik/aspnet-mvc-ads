using Microsoft.AspNetCore.Identity;

namespace Ads.Web.Mvc.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "DuplicateUserName",
                Description = $"Bu {userName} daha önce başka bir kullanıcı tarafından alınmıştır."

            };

        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = "InvalidUserName",
                Description = $"Bu {userName} kullanıcı adı geçersizdir."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new()
            {
                Code = "DuplicateEmail",
                Description = $"Bu '{email}' adresi daha önce başka bir kullanıcı tarafından kullanılmıştır."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre en az 8 karekterli olmalıdır." };
        }
    }
}
