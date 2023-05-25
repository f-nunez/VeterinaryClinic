using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class RoomCreatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Room _room;

    public RoomCreatedIntegrationEventFactory(Room room)
    {
        _room = room;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new RoomCreatedIntegrationEvent
        {
            RoomId = _room.Id,
            RoomName = _room.Name
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.RoomCreated.ToString();
    }
}