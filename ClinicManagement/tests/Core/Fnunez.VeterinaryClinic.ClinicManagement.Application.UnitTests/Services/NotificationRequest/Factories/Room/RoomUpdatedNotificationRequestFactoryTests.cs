using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class RoomUpdatedNotificationRequestFactoryTests
{
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly RoomUpdatedNotificationRequestFactory _factory;
    private readonly int _roomId = 1;
    private readonly string _roomName = "a";
    private readonly string _userId = Guid.NewGuid().ToString();

    public RoomUpdatedNotificationRequestFactoryTests()
    {
        var room = new Room(_roomId, _roomName);

        _factory = new RoomUpdatedNotificationRequestFactory
        (
            room,
            _correlationId,
            _userId
        );
    }

    public void CreateNotificationRequest_ReturnsRoomUpdatedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<RoomUpdatedNotificationRequest>(actual);

        var notificationRequest = actual as RoomUpdatedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_roomId, notificationRequest?.Id);

        Assert.Equal(_roomName, notificationRequest?.Name);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_WithoutEntries_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.RoomUpdated.ToString(),
            notificationEvent
        );
    }
}