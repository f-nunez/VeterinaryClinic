using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class RoomUpdatedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Room _room;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public RoomUpdatedNotificationRequestFactory(
        Room room,
        Guid correlationId,
        string? userId)
    {
        _room = room;
        _correlationId = correlationId;
        _userId = userId;
    }

    public BaseNotificationRequest CreateNotificationRequest()
    {
        return new RoomUpdatedNotificationRequest
        {
            CorrelationId = _correlationId,
            Id = _room.Id,
            Name = _room.Name,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.RoomUpdated.ToString();
    }
}