using System.Net.Mail;

namespace Fnunez.VeterinaryClinic.EmailService.Api.Settings;

public interface IEmailSetting
{
    bool EnableSsl { get; }
    string From { get; }
    string Host { get; }
    string Password { get; }
    int Port { get; }
    SmtpDeliveryMethod SmtpDeliveryMethod { get; }
    string UserName { get; }
}