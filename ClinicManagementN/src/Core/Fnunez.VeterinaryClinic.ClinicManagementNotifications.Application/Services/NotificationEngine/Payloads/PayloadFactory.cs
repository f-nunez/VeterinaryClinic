using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;

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
            case NotificationEvent.AppointmentTypeCreated:
                return _mapper.Map<AppointmentTypeCreatedPayload>(notificationRequest);
            case NotificationEvent.AppointmentTypeDeleted:
                return _mapper.Map<AppointmentTypeDeletedPayload>(notificationRequest);
            case NotificationEvent.AppointmentTypeUpdated:
                return _mapper.Map<AppointmentTypeUpdatedPayload>(notificationRequest);
            case NotificationEvent.ClientCreated:
                return _mapper.Map<ClientCreatedPayload>(notificationRequest);
            case NotificationEvent.ClientDeleted:
                return _mapper.Map<ClientDeletedPayload>(notificationRequest);
            case NotificationEvent.ClientUpdated:
                return _mapper.Map<ClientUpdatedPayload>(notificationRequest);
            case NotificationEvent.ClinicCreated:
                return _mapper.Map<ClinicCreatedPayload>(notificationRequest);
            case NotificationEvent.ClinicDeleted:
                return _mapper.Map<ClinicDeletedPayload>(notificationRequest);
            case NotificationEvent.ClinicUpdated:
                return _mapper.Map<ClinicUpdatedPayload>(notificationRequest);
            case NotificationEvent.DoctorCreated:
                return _mapper.Map<DoctorCreatedPayload>(notificationRequest);
            case NotificationEvent.DoctorDeleted:
                return _mapper.Map<DoctorDeletedPayload>(notificationRequest);
            case NotificationEvent.DoctorUpdated:
                return _mapper.Map<DoctorUpdatedPayload>(notificationRequest);
            case NotificationEvent.PatientCreated:
                return _mapper.Map<PatientCreatedPayload>(notificationRequest);
            case NotificationEvent.PatientDeleted:
                return _mapper.Map<PatientDeletedPayload>(notificationRequest);
            case NotificationEvent.PatientUpdated:
                return _mapper.Map<PatientUpdatedPayload>(notificationRequest);
            case NotificationEvent.RoomCreated:
                return _mapper.Map<RoomCreatedPayload>(notificationRequest);
            case NotificationEvent.RoomDeleted:
                return _mapper.Map<RoomDeletedPayload>(notificationRequest);
            case NotificationEvent.RoomUpdated:
                return _mapper.Map<RoomUpdatedPayload>(notificationRequest);
            default:
                throw new ArgumentException(
                    $"{nameof(notificationEvent)} not found with value: {notificationEvent}");
        }
    }
}