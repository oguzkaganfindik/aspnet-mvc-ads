using Ads.Domain.Entities.Concrete;
using System.Net;
using System.Net.Mail;

namespace Ads.Web.Mvc.Utils
{
    //public class MailHelper
    //{
    //    public static async Task SendMailAsync(User user)
    //    {
    //        SmtpClient smtpClient = new SmtpClient("mail.siteadresi.com", 587);
    //        smtpClient.Credentials = new NetworkCredential("emailKullaniciad", "emailsifre");
    //        smtpClient.EnableSsl = false; //gmail ise true yapılmalı
    //        MailMessage message = new MailMessage();
    //        message.From = new MailAddress("info@siteadi.com");
    //        message.To.Add("info@siteadi.com");
    //        message.To.Add("bilgi@siteadi.com");
    //        message.Subject = "Siteden mesaj geldi";
    //        message.Body = $"Mail Bilgileri <hr/> Ad Soyad: {musteri.Adi} {musteri.Soyadi} <hr/> İlgilendiği Advert Id : {musteri.AdvertId} <hr/> Email : {musteri.Email} <hr/> Telefon : {musteri.Telefon} <hr/> Notlar : {musteri.Notlar}";
    //        message.IsBodyHtml = true;
    //        smtpClient.Send(message);
    //        // smtpClient.Send(message);
    //        await smtpClient.SendMailAsync(message);
    //        smtpClient.Dispose();
    //    }
    //}
}
