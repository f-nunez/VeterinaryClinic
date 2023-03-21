using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class ClientUpdatedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Client _client;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public ClientUpdatedNotificationRequestFactory(
        Client client,
        Guid correlationId,
        string? userId)
    {
        _client = client;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new ClientUpdatedNotificationRequest
        {
            CorrelationId = _correlationId,
            FullName = _client.FullName,
            Id = _client.Id,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.ClientUpdated.ToString();
    }
}