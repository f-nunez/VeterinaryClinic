using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;

public class AppointmentDeletedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Appointment _appointment;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public AppointmentDeletedNotificationRequestFactory(
        Appointment appointment,
        Guid correlationId,
        string? userId)
    {
        _appointment = appointment;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new AppointmentDeletedNotificationRequest
        {
            CorrelationId = _correlationId,
            Id = _appointment.Id,
            Title = _appointment.Title,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.AppointmentDeleted.ToString();
    }
}