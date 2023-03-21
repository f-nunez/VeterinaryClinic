using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class ClinicCreatedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Clinic _clinic;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public ClinicCreatedNotificationRequestFactory(
        Clinic clinic,
        Guid correlationId,
        string? userId)
    {
        _clinic = clinic;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new ClinicCreatedNotificationRequest
        {
            CorrelationId = _correlationId,
            Id = _clinic.Id,
            Name = _clinic.Name,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.ClinicCreated.ToString();
    }
}