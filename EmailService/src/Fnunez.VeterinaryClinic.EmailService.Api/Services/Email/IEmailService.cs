namespace Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;

public interface IEmailService
{
    void SendEmail(EmailComposition emailComposition);
}