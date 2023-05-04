using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class DoctorUpdatedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Doctor _doctor;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public DoctorUpdatedNotificationRequestFactory(
        Doctor doctor,
        Guid correlationId,
        string? userId)
    {
        _doctor = doctor;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new DoctorUpdatedNotificationRequest
        {
            CorrelationId = _correlationId,
            FullName = _doctor.FullName,
            Id = _doctor.Id,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.DoctorUpdated.ToString();
    }
}