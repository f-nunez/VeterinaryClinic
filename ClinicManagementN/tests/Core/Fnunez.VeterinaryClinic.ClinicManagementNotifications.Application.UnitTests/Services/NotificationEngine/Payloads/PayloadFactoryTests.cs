using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.UnitTests.Services.NotificationEngine.Payloads;

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
    public void GetPayload_NotificationEventIsAppointmentTypeCreated_ReturnsAppointmentTypeCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentTypeCreated;

        var notificationRequest = new AppointmentTypeCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentTypeProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentTypeCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsAppointmentTypeDeleted_ReturnsAppointmentTypeDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentTypeDeleted;

        var notificationRequest = new AppointmentTypeDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentTypeProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentTypeDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsAppointmentTypeUpdated_ReturnsAppointmentTypeUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.AppointmentTypeUpdated;

        var notificationRequest = new AppointmentTypeUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentTypeProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<AppointmentTypeUpdatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsClientCreated_ReturnsClientCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClientCreated;

        var notificationRequest = new ClientCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClientProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClientCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsClientDeleted_ReturnsClientDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClientDeleted;

        var notificationRequest = new ClientDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClientProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClientDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsClientUpdated_ReturnsClientUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClientUpdated;

        var notificationRequest = new ClientUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClientProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClientUpdatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsClinicCreated_ReturnsClinicCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClinicCreated;

        var notificationRequest = new ClinicCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClinicProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClinicCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsClinicDeleted_ReturnsClinicDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClinicDeleted;

        var notificationRequest = new ClinicDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClinicProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClinicDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsClinicUpdated_ReturnsClinicUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.ClinicUpdated;

        var notificationRequest = new ClinicUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClinicProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<ClinicUpdatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsDoctorCreated_ReturnsDoctorCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.DoctorCreated;

        var notificationRequest = new DoctorCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new DoctorProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<DoctorCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsDoctorDeleted_ReturnsDoctorDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.DoctorDeleted;

        var notificationRequest = new DoctorDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new DoctorProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<DoctorDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsDoctorUpdated_ReturnsDoctorUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.DoctorUpdated;

        var notificationRequest = new DoctorUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new DoctorProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<DoctorUpdatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsPatientCreated_ReturnsPatientCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.PatientCreated;

        var notificationRequest = new PatientCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new PatientProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<PatientCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsPatientDeleted_ReturnsPatientDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.PatientDeleted;

        var notificationRequest = new PatientDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new PatientProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<PatientDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsPatientUpdated_ReturnsPatientUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.PatientUpdated;

        var notificationRequest = new PatientUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new PatientProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<PatientUpdatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsRoomCreated_ReturnsRoomCreatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.RoomCreated;

        var notificationRequest = new RoomCreatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new RoomProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<RoomCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsRoomDeleted_ReturnsRoomDeletedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.RoomDeleted;

        var notificationRequest = new RoomDeletedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new RoomProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<RoomDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_NotificationEventIsRoomUpdated_ReturnsRoomUpdatedPayload()
    {
        // Arrange
        var notificationEvent = NotificationEvent.RoomUpdated;

        var notificationRequest = new RoomUpdatedNotificationRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new RoomProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(notificationEvent, notificationRequest);

        // Assert
        Assert.IsType<RoomUpdatedPayload>(actual);
    }
}