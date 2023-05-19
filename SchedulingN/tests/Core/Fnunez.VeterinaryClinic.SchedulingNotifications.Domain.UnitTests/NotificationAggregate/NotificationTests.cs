using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.UnitTests.NotificationAggregate;

public class NotificationTests
{
    [Fact]
    public void Constructor_CorrelationId_SetsCorrelationIdProperty()
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        var triggeredByUserId = Guid.NewGuid().ToString();

        var notification = new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Equal(correlationId, notification.CorrelationId);
    }

    [Fact]
    public void Constructor_CorrelationIdIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var correlationId = Guid.Empty;

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        var triggeredByUserId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_CreatedOn_SetsCreatedOnProperty()
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        var triggeredByUserId = Guid.NewGuid().ToString();

        var notification = new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Equal(createdOn, notification.CreatedOn);
    }

    [Fact]
    public void Constructor_NotificationEvent_SetsNotificationEventProperty()
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        var triggeredByUserId = Guid.NewGuid().ToString();

        var notification = new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Equal(notificationEvent, notification.NotificationEvent);
    }

    [Fact]
    public void Constructor_Payload_SetsPayloadProperty()
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        var triggeredByUserId = Guid.NewGuid().ToString();

        var notification = new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Equal(payload, notification.Payload);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_PayloadIsEmpty_ThrowsArgumentException(
        string payload)
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var triggeredByUserId = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_TriggeredByUserId_SetsTriggeredByUserIdProperty()
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        var triggeredByUserId = Guid.NewGuid().ToString();

        var notification = new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Equal(triggeredByUserId, notification.TriggeredByUserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_TriggeredByUserIdIsEmpty_ThrowsArgumentException(
        string triggeredByUserId)
    {
        // Arrange
        var correlationId = Guid.NewGuid();

        var createdOn = DateTimeOffset.UtcNow;

        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var payload = "payload";

        // Act
        Action actual = () => new Notification
        (
            correlationId,
            createdOn,
            notificationEvent,
            payload,
            triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}