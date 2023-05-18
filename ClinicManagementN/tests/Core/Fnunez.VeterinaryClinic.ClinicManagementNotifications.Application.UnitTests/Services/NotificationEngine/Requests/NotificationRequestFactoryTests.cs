using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.UnitTests.Services.NotificationEngine.Requests;

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
    public void GetNotificationRequest_NotificationEventIsAppointmentTypeCreated_ReturnsAppointmentTypeCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentTypeCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentTypeCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentTypeCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsAppointmentTypeDeleted_ReturnsAppointmentTypeDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentTypeDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentTypeDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentTypeDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsAppointmentTypeUpdated_ReturnsAppointmentTypeUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentTypeUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new AppointmentTypeUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentTypeUpdatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsClientCreated_ReturnsClientCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClientCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new ClientCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClientCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsClientDeleted_ReturnsClientDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClientDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new ClientDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClientDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsClientUpdated_ReturnsClientUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClientUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new ClientUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClientUpdatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsClinicCreated_ReturnsClinicCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClinicCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new ClinicCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClinicCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsClinicDeleted_ReturnsClinicDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClinicDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new ClinicDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClinicDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsClinicUpdated_ReturnsClinicUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClinicUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new ClinicUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClinicUpdatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsDoctorCreated_ReturnsDoctorCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.DoctorCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new DoctorCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<DoctorCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsDoctorDeleted_ReturnsDoctorDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.DoctorDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new DoctorDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<DoctorDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsDoctorUpdated_ReturnsDoctorUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.DoctorUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new DoctorUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<DoctorUpdatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsPatientCreated_ReturnsPatientCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.PatientCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new PatientCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<PatientCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsPatientDeleted_ReturnsPatientDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.PatientDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new PatientDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<PatientDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsPatientUpdated_ReturnsPatientUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.PatientUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new PatientUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<PatientUpdatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsRoomCreated_ReturnsRoomCreatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.RoomCreated;

        var notificationRequest = JsonSerializer.Serialize(
            new RoomCreatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<RoomCreatedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsRoomDeleted_ReturnsRoomDeletedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.RoomDeleted;

        var notificationRequest = JsonSerializer.Serialize(
            new RoomDeletedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<RoomDeletedNotificationRequest>(actual);
    }

    [Fact]
    public void GetNotificationRequest_NotificationEventIsRoomUpdated_ReturnsRoomUpdatedNotificationRequest()
    {
        // Arrange
        var notificationEvent = NotificationEvent.RoomUpdated;

        var notificationRequest = JsonSerializer.Serialize(
            new RoomUpdatedNotificationRequest());

        // Act
        var actual = _factory.GetNotificationRequest(
            notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<RoomUpdatedNotificationRequest>(actual);
    }
}