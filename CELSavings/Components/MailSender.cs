using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace CELSavings
{
    public class MailSender 
    {
       
        //private static const

        public static void SendAccountConfirmationMail(string destinationEmail,string callbackUrl)
        {
            string text = string.Format("Please click on this link to confirm your account: {0}", callbackUrl);
            string html = "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a><br/><br/>";
            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + callbackUrl);
            
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(destinationEmail));
            msg.Subject = "'CEL Savings Association' account confirmation";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            SendMail(msg);
        }

        public static void SendPasswordResetMail(string destinationEmail, string callbackUrl)
        {
            string text = string.Format("Please click on this link to reset your account password: {0}", callbackUrl);
            string html = "Please reset your account password by clicking this link: <a href=\"" + callbackUrl + "\">link</a><br/><br/>";
            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + callbackUrl);

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(destinationEmail));
            msg.Subject = "'CEL Savings Association' password reset";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            SendMail(msg);
        }


        private static void SendMail(MailMessage mailMessage)
        {
            mailMessage.From = new MailAddress("celsavingsassosiation.info@gmail.com");

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("celsavingsassosiation.info@gmail.com", "Cel1@3$5^");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}