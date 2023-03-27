using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;

public interface IPayloadFactory
{
    public BasePayload GetPayload(EmailEvent emailEvent, BaseEmailRequest emailRequest);
}