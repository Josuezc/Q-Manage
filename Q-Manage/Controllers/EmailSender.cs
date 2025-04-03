using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Q_Manage.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly MailSettings _mailSettings;

    public EmailSender(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mail.To.Add(email);

        using var smtp = new SmtpClient(_mailSettings.SmtpServer, _mailSettings.Port)
        {
            Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password),
            EnableSsl = true,
        };
       await smtp.SendMailAsync(mail);
    }
}