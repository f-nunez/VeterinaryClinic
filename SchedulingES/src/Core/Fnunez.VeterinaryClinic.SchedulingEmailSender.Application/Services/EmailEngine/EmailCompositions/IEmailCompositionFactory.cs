using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.EmailCompositions;

public interface IEmailCompositionFactory
{
    Task<EmailComposition> GetEmailCompositionAsync(EmailEvent emailEvent, BasePayload payload);
}