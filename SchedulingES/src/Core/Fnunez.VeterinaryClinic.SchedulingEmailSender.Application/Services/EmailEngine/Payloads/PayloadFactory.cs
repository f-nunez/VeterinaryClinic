using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;

public class PayloadFactory : IPayloadFactory
{
    private readonly IMapper _mapper;

    public PayloadFactory(IMapper mapper)
    {
        _mapper = mapper;
    }

    public BasePayload GetPayload(
        EmailEvent emailEvent,
        BaseEmailRequest emailRequest)
    {
        switch (emailEvent)
        {
            case EmailEvent.AppointmentConfirmed:
                return _mapper.Map<AppointmentConfirmedPayload>(emailRequest);
            case EmailEvent.AppointmentCreated:
                return _mapper.Map<AppointmentCreatedPayload>(emailRequest);
            case EmailEvent.AppointmentDeleted:
                return _mapper.Map<AppointmentDeletedPayload>(emailRequest);
            case EmailEvent.AppointmentUpdated:
                return _mapper.Map<AppointmentUpdatedPayload>(emailRequest);
            default:
                throw new ArgumentException(
                    $"{nameof(emailEvent)} not found with value: {emailEvent}");
        }
    }
}