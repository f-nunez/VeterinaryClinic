using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;

public class AppointmentUpdatedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Appointment _appointment;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public AppointmentUpdatedNotificationRequestFactory(
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
        return new AppointmentUpdatedNotificationRequest
        {
            CorrelationId = _correlationId,
            Id = _appointment.Id,
            Title = _appointment.Title,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.AppointmentUpdated.ToString();
    }
}