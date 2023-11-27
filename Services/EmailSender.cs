using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace E_Nompilo_Healthcare_system.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender() { }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            string from = "mafhuwaronewa62@gmail.com";
            string Password = "Ronewa@203";
            string SenderName = "Admin";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.Body = "<htm><body>" + htmlMessage + "</body></html>";
            message.To.Add(new MailAddress(from));
            message.IsBodyHtml = true;

            var smtpclient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(from, Password),
                EnableSsl = true
            };
            smtpclient.Send(message);
        }

    }
}
