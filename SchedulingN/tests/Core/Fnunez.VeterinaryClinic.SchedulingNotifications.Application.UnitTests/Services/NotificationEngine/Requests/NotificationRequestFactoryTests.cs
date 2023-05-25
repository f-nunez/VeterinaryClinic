using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Services.NotificationEngine.Requests;

public class NotificationRequestFactoryTests
{
    private readonly NotificationRequestFactory _factory;

    public NotificationRequestFactoryTests()
    {
        _factory = new();
    }

    [Fact]
    public void GetNotificationRequest_AllNotificationEventsAreFound_IsValid()
    {
        // Arrange
        var serializedNotificationRequest = "serializedNotificationRequest";

        var allNotificationEvents = Enum.GetValues(typeof(NotificationEvent))
            .Cast<NotificationEvent>();

        var allNotificationEventsAreFound = true;

        // Act
        foreach (var notificationEvent in allNotificationEvents)
        {
            try
            {
                _factory.GetNotificationRequest(notificationEvent, serializedNotificationRequest);
            }
            catch (Exception ex)
            {
                if (ex.Message == $"{nameof(notificationEvent)} not found with value: {notificationEvent}")
                {
                    allNotificationEventsAreFound = false;
                    break;
                }
            }
        }

        // Assert
        Assert.True(allNotificationEventsAreFound);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsAppointmentConfirmed_ReturnsAppointmentConfirmedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentConfirmedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentConfirmedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsAppointmentCreated_ReturnsAppointmentCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsAppointmentDeleted_ReturnsAppointmentDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsAppointmentUpdated_ReturnsAppointmentUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentUpdatedNotificationRequest>(actual);
    }
}