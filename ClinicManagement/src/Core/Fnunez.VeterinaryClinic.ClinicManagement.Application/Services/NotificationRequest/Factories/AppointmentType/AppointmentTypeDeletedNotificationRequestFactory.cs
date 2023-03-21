using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class AppointmentTypeDeletedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly AppointmentType _appointmentType;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public AppointmentTypeDeletedNotificationRequestFactory(
        AppointmentType appointmentType,
        Guid correlationId,
        string? userId)
    {
        _appointmentType = appointmentType;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new AppointmentTypeDeletedNotificationRequest
        {
            CorrelationId = _correlationId,
            Id = _appointmentType.Id,
            Name = _appointmentType.Name,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.AppointmentTypeDeleted.ToString();
    }
}