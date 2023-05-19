using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Mappings;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Services.NotificationEngine.Payloads;

public class PayloadFactoryTests
{
    [Fact]
    public void GetPayload_AllNotificationEventsAreFound_IsValid()
    {
        // Arrange
        BaseNotificationRequest? notificationRequest = null;

        var allNotificationEvents = Enum.GetValues(typeof(NotificationEvent))
            .Cast<NotificationEvent>();

        var allNotificationEventsAreFound = true;

        var mockIMapper = new Mock<IMapper>();

        // Act
        foreach (var notificationEvent in allNotificationEvents)
        {
            try
            {
                var factory = new PayloadFactory(mockIMapper.Object);

                factory.GetPayload(notificationEvent, notificationRequest!);
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
    public void GetPayload_NotificationEventIsAppointmentConfirmed_ReturnsAppointmentConfirmedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentConfirmed;

        var notificationRequest = new AppointmentConfirmedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentConfirmedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsAppointmentCreated_ReturnsAppointmentCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentCreated;

        var notificationRequest = new AppointmentCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsAppointmentDeleted_ReturnsAppointmentDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentDeleted;

        var notificationRequest = new AppointmentDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsAppointmentUpdated_ReturnsAppointmentUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentUpdated;

        var notificationRequest = new AppointmentUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentUpdatedPayload>(actual);
    }
}