using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public class RoomCreatedNotificationRequestFactory
    : INotificationRequestFactory
{
    private readonly Room _room;
    private readonly Guid _correlationId;
    private readonly string? _userId;

    public RoomCreatedNotificationRequestFactory(
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
        return new RoomCreatedNotificationRequest
        {
            CorrelationId = _correlationId,
            Id = _room.Id,
            Name = _room.Name,
            TriggeredByUserId = _userId
        };
    }

    public string GetNotificationEvent()
    {
        return NotificationEvent.RoomCreated.ToString();
    }
}