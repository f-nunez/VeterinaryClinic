using System.Net.Mail;

namespace Fnunez.VeterinaryClinic.EmailService.Api.Settings;

public class EmailSetting : IEmailSetting
{
    public bool EnableSsl { get; set; }
    public string From { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Port { get; set; }
    public int RetryCount { get; set; }
    public int RetryTimeInSeconds { get; set; }
    public SmtpDeliveryMethod SmtpDeliveryMethod { get; set; }
    public string UserName { get; set; } = null!;
}