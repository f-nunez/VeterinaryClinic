using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class RoomDeletedIntegrationEventFactoryTests
{
    private readonly RoomDeletedIntegrationEventFactory _factory;
    private readonly int _roomId = 1;
    private readonly string _roomName = "a";

    public RoomDeletedIntegrationEventFactoryTests()
    {
        var room = new Room(_roomId, _roomName);

        _factory = new RoomDeletedIntegrationEventFactory(room);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsRoomDeletedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<RoomDeletedIntegrationEvent>(actual);

        var integrationEvent = actual as RoomDeletedIntegrationEvent;

        Assert.Equal(_roomId, integrationEvent?.RoomId);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsRoomDeleted()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.RoomDeleted.ToString(),
            integrationEvent
        );
    }
}