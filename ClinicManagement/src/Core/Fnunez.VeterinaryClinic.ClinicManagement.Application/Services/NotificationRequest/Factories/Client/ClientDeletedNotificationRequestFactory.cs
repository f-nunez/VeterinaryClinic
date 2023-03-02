using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class ClientDeletedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Client _client;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public ClientDeletedNotificationRequestFactory(
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
        return new ClientDeletedNotificationRequest
        {
            CorrelationId = _correlationId,
            FullName = _client.FullName,
            Id = _client.Id,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.ClientDeleted.ToString();
    }
}