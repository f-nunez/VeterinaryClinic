namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.EmailTemplate;

public interface IEmailTemplateService
{
    Task<string> GetConfirmationUrlAsync(Guid appointmentId, string? language);
}