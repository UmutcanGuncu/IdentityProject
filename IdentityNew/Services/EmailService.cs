using System;
using System.Net;
using System.Net.Mail;
using IdentityNew.OptionsModel;
using Microsoft.Extensions.Options;

namespace IdentityNew.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordEmail(string resetEmailLink, string To)
        {
            var smptClient = new SmtpClient();
            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email,_emailSettings.Password);
            smptClient.EnableSsl = true;
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(To);
            mailMessage.Subject = "Şifre Sıfırlama Linki";
            mailMessage.Body = $"{resetEmailLink}";
            mailMessage.IsBodyHtml = true;
            await smptClient.SendMailAsync(mailMessage);
        }
    }
}

