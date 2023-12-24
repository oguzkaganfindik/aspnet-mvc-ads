//using Ads.Domain.Entities.Concrete;
//using Ads.Web.Mvc.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace Ads.Web.Mvc.Controllers
//{
//    public class xForgotPasswordController : Controller
//    {
//        private readonly UserManager<User> _userManager;

//        public xForgotPasswordController(UserManager<User> userManager)
//        {
//            _userManager = userManager;
//        }

//        public IActionResult ForgotPassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> ForgotPassword(xForgotPasswordViewModel forgotPasswordViewModel)
//        {
//            //var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Mail);
//            //string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
//            //var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
//            //{
//            //    userId = user.Id,
//            //    token = passwordResetToken
//            //}, HttpContext.Request.Scheme);

//            //MimeMessage mimeMessage = new MimeMessage();

//            //MailboxAddress mailboxAdressFrom = new MailboxAddress("Admin", "Emailadres@gmail.com");

//            //mimeMessage.From.Add(mailboxAdressFrom);

//            //MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
//            //mimeMessage.To.Add(mailboxAdressTo);

//            //var bodyBuilder = new BodyBuilder();
//            //bodyBuilder.TextBody = mailRequest.Body;
//            //mimeMessage.Body = bodyBuilder.ToMessageBody();

//            //mimeMessage.Subject = mailRequest.Subject;


//            return View();
//        }
//    }
//}
