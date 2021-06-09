using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;

namespace BusinessLayer.Helpers
{
    public class EmaillHelper
    {
        public bool SendEmailPasswordReset(IConfiguration configuration, string userEmail, string link)
        {
            var _email = configuration["EmailCredentials:EmailAdress"];
            var _password = configuration["EmailCredentials:EmailPassword"];

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_email);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_email, _password);
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
