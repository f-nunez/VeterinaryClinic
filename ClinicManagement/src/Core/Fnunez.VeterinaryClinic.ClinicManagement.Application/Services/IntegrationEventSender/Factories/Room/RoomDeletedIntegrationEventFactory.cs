using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class RoomDeletedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Room _room;

    public RoomDeletedIntegrationEventFactory(Room room)
    {
        _room = room;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new RoomDeletedIntegrationEvent
        {
            RoomId = _room.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.RoomDeleted.ToString();
    }
}