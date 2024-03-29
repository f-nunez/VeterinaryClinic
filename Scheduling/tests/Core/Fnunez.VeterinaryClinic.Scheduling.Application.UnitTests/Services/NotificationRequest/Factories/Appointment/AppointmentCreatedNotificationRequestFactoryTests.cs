using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Services.NotificationRequest.Factories;

public class AppointmentCreatedNotificationRequestFactoryTests
{
    private readonly int _appointmentTypeId = 1;
    private readonly int _clientId = 1;
    private readonly int _clinicId = 1;
    private readonly DateTimeOffset _confirmOn = DateTimeOffset.UtcNow.AddDays(1);
    private readonly DateTimeOffset _dateRangeStartOn = DateTimeOffset.UtcNow;
    private readonly DateTimeOffset _dateRangeEndOn = DateTimeOffset.UtcNow.AddMinutes(1);
    private readonly string _description = "a";
    private readonly int _doctorId = 1;
    private readonly Guid _id = Guid.NewGuid();
    private readonly int _patientId = 1;
    private readonly int _roomId = 1;
    private readonly string _title = "a";

    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly AppointmentCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public AppointmentCreatedNotificationRequestFactoryTests()
    {
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        _factory = new AppointmentCreatedNotificationRequestFactory
        (
            appointment,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsAppointmentCreatedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest()
            as AppointmentCreatedNotificationRequest;

        // Assert
        Assert.IsType<AppointmentCreatedNotificationRequest>(actual);

        var notificationRequest = actual as AppointmentCreatedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_id, notificationRequest?.Id);

        Assert.Equal(_title, notificationRequest?.Title);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsAppointmentCreated()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.AppointmentCreated.ToString(),
            notificationEvent
        );
    }
}