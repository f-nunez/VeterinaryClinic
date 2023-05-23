using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class RoomUpdatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Room _room;

    public RoomUpdatedIntegrationEventFactory(Room room)
    {
        _room = room;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new RoomUpdatedIntegrationEvent
        {
            RoomId = _room.Id,
            RoomName = _room.Name
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.RoomUpdated.ToString();
    }
}