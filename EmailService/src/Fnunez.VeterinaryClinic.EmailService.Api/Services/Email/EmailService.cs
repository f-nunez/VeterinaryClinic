using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using Fnunez.VeterinaryClinic.EmailService.Api.Settings;

namespace Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;

public class EmailService : IEmailService
{
    private readonly IEmailSetting _emailSetting;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IEmailSetting emailSetting,
        ILogger<EmailService> logger)
    {
        _emailSetting = emailSetting;
        _logger = logger;
    }

    public void SendEmail(EmailComposition emailComposition)
    {
        ValidateEmailComposition(emailComposition);

        int retry = 0;

        do
        {
            if (retry > 0)
                Thread.Sleep(_emailSetting.RetryTimeInSeconds * 1000);

            if (TrySendEmail(emailComposition))
                return;
        }
        while (_emailSetting.RetryCount > 0 && retry++ < _emailSetting.RetryCount);
    }

    private bool TrySendEmail(EmailComposition emailComposition)
    {
        bool hasBeenSent = false;

        try
        {
            using (var mailMessage = new MailMessage())
            using (var smtpClient = new SmtpClient())
            {
                SetMailMessage(mailMessage, emailComposition);
                SetSmtpClient(smtpClient);
                smtpClient.Send(mailMessage);
            }

            hasBeenSent = true;
        }
        catch (SmtpFailedRecipientsException ex)
        {
            for (int i = 0; i < ex.InnerExceptions.Length; i++)
            {
                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;

                if (status == SmtpStatusCode.MailboxBusy ||
                    status == SmtpStatusCode.MailboxUnavailable)
                    _logger.LogError($"Delivery failed to: {ex.InnerExceptions[i].FailedRecipient}", ex);
                else
                    _logger.LogError($"Failed to deliver message to: {ex.InnerExceptions[i].FailedRecipient}", ex);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return hasBeenSent;
    }

    private void SetMailMessage(
        MailMessage mailMessage,
        EmailComposition emailComposition)
    {
        mailMessage.Body = emailComposition.Body;

        mailMessage.IsBodyHtml = emailComposition.IsBodyHtml;

        mailMessage.From = new MailAddress(_emailSetting.From);

        mailMessage.Subject = emailComposition.Subject;

        mailMessage.To.Add(new MailAddress(emailComposition.To!.Replace(";", ",")));
    }

    private void SetSmtpClient(SmtpClient smtpClient)
    {
        smtpClient.Credentials = new NetworkCredential(
            _emailSetting.UserName, _emailSetting.Password);

        smtpClient.DeliveryMethod = _emailSetting.SmtpDeliveryMethod;

        smtpClient.EnableSsl = _emailSetting.EnableSsl;

        smtpClient.Host = _emailSetting.Host;

        smtpClient.Port = _emailSetting.Port;
    }

    private static void ValidateEmailComposition(EmailComposition emailComposition)
    {
        if (string.IsNullOrEmpty(emailComposition.To))
            throw new ArgumentException(
                $"{nameof(emailComposition.To)} is empty.");

        if (string.IsNullOrEmpty(emailComposition.Subject))
            throw new ArgumentException(
                $"{nameof(emailComposition.Subject)} is empty.");

        if (string.IsNullOrEmpty(emailComposition.Body))
            throw new ArgumentException(
                $"{nameof(emailComposition.Body)} is empty.");
    }
}