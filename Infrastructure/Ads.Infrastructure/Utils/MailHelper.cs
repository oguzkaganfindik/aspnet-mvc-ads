using Ads.Domain.Entities.Concrete;
using System.Net;
using System.Net.Mail;

namespace Ads.Web.Mvc.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Customer customer)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadresi.com", 587);
            smtpClient.Credentials = new NetworkCredential("emailKullaniciad", "emailsifre");
            smtpClient.EnableSsl = false; //gmail ise true yapılmalı
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadi.com");
            message.To.Add("info@siteadi.com");
            message.To.Add("bilgi@siteadi.com");
            message.Subject = "Siteden mesaj geldi";
            message.Body = $"Mail Bilgileri <hr/> Ad Soyad: {customer.FirstName} {customer.LastName} <hr/> İlgilendiği Advert Id : {customer.AdvertId} <hr/> Email : {customer.Email} <hr/> Telefon : {customer.Phone} <hr/> Notlar : {customer.Notes}";
            message.IsBodyHtml = true;
            smtpClient.Send(message);
            // smtpClient.Send(message);
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
