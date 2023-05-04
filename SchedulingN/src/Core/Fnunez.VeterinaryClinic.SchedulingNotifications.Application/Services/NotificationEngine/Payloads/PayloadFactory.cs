using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;

public class PayloadFactory : IPayloadFactory
{
    private readonly IMapper _mapper;

    public PayloadFactory(IMapper mapper)
    {
        _mapper = mapper;
    }

    public BasePayload GetPayload(
        NotificationEvent notificationEvent,
        BaseNotificationRequest notificationRequest)
    {
        switch (notificationEvent)
        {
            case NotificationEvent.AppointmentConfirmed:
                return _mapper.Map<AppointmentConfirmedPayload>(notificationRequest);
            case NotificationEvent.AppointmentCreated:
                return _mapper.Map<AppointmentCreatedPayload>(notificationRequest);
            case NotificationEvent.AppointmentDeleted:
                return _mapper.Map<AppointmentDeletedPayload>(notificationRequest);
            case NotificationEvent.AppointmentUpdated:
                return _mapper.Map<AppointmentUpdatedPayload>(notificationRequest);
            default:
                throw new ArgumentException(
                    $"{nameof(notificationEvent)} not found with value: {notificationEvent}");
        }
    }
}