using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class RoomCreatedNotificationRequestFactoryTests
{
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly RoomCreatedNotificationRequestFactory _factory;
    private readonly int _roomId = 1;
    private readonly string _roomName = "a";
    private readonly string _userId = Guid.NewGuid().ToString();

    public RoomCreatedNotificationRequestFactoryTests()
    {
        var room = new Room(_roomId, _roomName);

        _factory = new RoomCreatedNotificationRequestFactory
        (
            room,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsRoomCreatedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<RoomCreatedNotificationRequest>(actual);

        var notificationRequest = actual as RoomCreatedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_roomId, notificationRequest?.Id);

        Assert.Equal(_roomName, notificationRequest?.Name);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.RoomCreated.ToString(),
            notificationEvent
        );
    }
}