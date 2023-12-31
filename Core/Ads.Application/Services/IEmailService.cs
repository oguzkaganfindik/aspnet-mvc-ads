namespace Ads.Application.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetPasswordEmailLink, string toEmail);
    }
}
