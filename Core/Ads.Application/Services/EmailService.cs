using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ads.Application.OptionsModel;

namespace Ads.Application.Services
{
    public class EmailService :IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;  //program cse git
        }
        public async Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail)
        {
            var smtpClient = new SmtpClient();

            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);

            mailMessage.Subject = " Localhost | Şifre sıfırlama linki";
            mailMessage.Body = @$"<h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4>
                    <p>
                    <a href='{resetPasswordEmailLink}'> şifre yenileme link </a>
                    </p>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
