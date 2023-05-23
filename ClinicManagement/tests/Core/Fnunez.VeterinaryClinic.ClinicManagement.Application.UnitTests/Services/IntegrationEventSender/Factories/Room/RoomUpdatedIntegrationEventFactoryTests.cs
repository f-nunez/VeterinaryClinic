using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class RoomUpdatedIntegrationEventFactoryTests
{
    private readonly RoomUpdatedIntegrationEventFactory _factory;
    private readonly int _roomId = 1;
    private readonly string _roomName = "a";

    public RoomUpdatedIntegrationEventFactoryTests()
    {
        var room = new Room(_roomId, _roomName);

        _factory = new RoomUpdatedIntegrationEventFactory(room);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsRoomUpdatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<RoomUpdatedIntegrationEvent>(actual);

        var integrationEvent = actual as RoomUpdatedIntegrationEvent;

        Assert.Equal(_roomId, integrationEvent?.RoomId);

        Assert.Equal(_roomName, integrationEvent?.RoomName);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsRoomUpdated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.RoomUpdated.ToString(),
            integrationEvent
        );
    }
}