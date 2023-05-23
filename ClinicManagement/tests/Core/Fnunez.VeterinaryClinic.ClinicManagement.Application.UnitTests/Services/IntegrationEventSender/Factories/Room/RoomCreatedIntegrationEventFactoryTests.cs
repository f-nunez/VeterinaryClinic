using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class RoomCreatedIntegrationEventFactoryTests
{
    private readonly RoomCreatedIntegrationEventFactory _factory;
    private readonly int _roomId = 1;
    private readonly string _roomName = "a";

    public RoomCreatedIntegrationEventFactoryTests()
    {
        var room = new Room(_roomId, _roomName);

        _factory = new RoomCreatedIntegrationEventFactory(room);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsRoomCreatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<RoomCreatedIntegrationEvent>(actual);

        var integrationEvent = actual as RoomCreatedIntegrationEvent;

        Assert.Equal(_roomId, integrationEvent?.RoomId);

        Assert.Equal(_roomName, integrationEvent?.RoomName);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsRoomCreated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.RoomCreated.ToString(),
            integrationEvent
        );
    }
}