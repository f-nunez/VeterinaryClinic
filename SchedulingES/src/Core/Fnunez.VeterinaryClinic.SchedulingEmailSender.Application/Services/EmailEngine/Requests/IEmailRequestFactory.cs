using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;

public interface IEmailRequestFactory
{
    public BaseEmailRequest GetEmailRequest(EmailEvent emailEvent, string serializedEmailRequest);
}