using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class AppointmentTypeDeletedNotificationRequestFactoryTests
{
    private readonly string _appointmentTypeCode = "c";
    private readonly int _appointmentTypeDuration = 1;
    private readonly int _appointmentTypeId = 1;
    private readonly string _appointmentTypeName = "n";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly AppointmentTypeDeletedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public AppointmentTypeDeletedNotificationRequestFactoryTests()
    {
        var appointmentType = new AppointmentType
        (
            _appointmentTypeId,
            _appointmentTypeName,
            _appointmentTypeCode,
            _appointmentTypeDuration
        );

        _factory = new AppointmentTypeDeletedNotificationRequestFactory
        (
            appointmentType,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsAppointmentTypeDeletedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<AppointmentTypeDeletedNotificationRequest>(actual);

        var notificationRequest = actual as AppointmentTypeDeletedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_appointmentTypeId, notificationRequest?.Id);

        Assert.Equal(_appointmentTypeName, notificationRequest?.Name);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.AppointmentTypeDeleted.ToString(),
            notificationEvent
        );
    }
}