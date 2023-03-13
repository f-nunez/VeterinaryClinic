using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class PatientDeletedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Patient _patient;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public PatientDeletedNotificationRequestFactory(
        Patient patient,
        Guid correlationId,
        string? userId)
    {
        _patient = patient;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new PatientDeletedNotificationRequest
        {
            ClientId = _patient.ClientId,
            CorrelationId = _correlationId,
            PatientId = _patient.Id,
            Name = _patient.Name,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.PatientDeleted.ToString();
    }
}