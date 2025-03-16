using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using NexCart.Services.Interfaces;
/*using NexCart.Services.Interfaces;*/

namespace NexCart.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*public void SendEmail(string toEmail, string subject, string body)*/
        /*{*/
        /*    var smtpServer = _configuration["EmailSettings:SmtpServer"];*/
        /*    var port = int.Parse(_configuration["EmailSettings:Port"]);*/
        /*    var senderEmail = _configuration["EmailSettings:SenderEmail"];*/
        /*    var senderPassword = _configuration["EmailSettings:SenderPassword"];*/
        /*    var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSSL"]);*/
        /**/
        /*    using (MailMessage mail = new MailMessage())*/
        /*    {*/
        /*        mail.From = new MailAddress(senderEmail);*/
        /*        mail.To.Add(toEmail);*/
        /*        mail.Subject = subject;*/
        /*        mail.Body = body;*/
        /*        mail.IsBodyHtml = true;*/
        /**/
        /*        using (SmtpClient smtp = new SmtpClient(smtpServer, port))*/
        /*        {*/
        /*            smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);*/
        /*            smtp.EnableSsl = enableSsl;*/
        /*            smtp.Send(mail);*/
        /*        }*/
        /*    }*/
        /*}*/

        public bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"]);
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderPassword = _configuration["EmailSettings:SenderPassword"];
                var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSSL"]);

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpServer, port))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = enableSsl;
                        smtp.Send(mail);
                    }
                }

                Console.WriteLine($"✅ Email sent successfully to {toEmail}");
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"❌ SMTP Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Email sending failed: {ex.Message}");
            }

            return false;
        }
    }
}

