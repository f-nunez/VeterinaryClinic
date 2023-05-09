using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class AppointmentTypeCreatedNotificationRequestFactoryTests
{
    private readonly string _appointmentTypeCode = "c";
    private readonly int _appointmentTypeDuration = 1;
    private readonly int _appointmentTypeId = 1;
    private readonly string _appointmentTypeName = "n";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly AppointmentTypeCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public AppointmentTypeCreatedNotificationRequestFactoryTests()
    {
        var appointmentType = new AppointmentType(
            _appointmentTypeId,
            _appointmentTypeName,
            _appointmentTypeCode,
            _appointmentTypeDuration
        );

        _factory = new AppointmentTypeCreatedNotificationRequestFactory(
            appointmentType,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void GetNotificationEvent_WithoutEntries_ReturnsNotificationEvent()
    {
        // Act
        var notificationRequest = _factory.CreateNotificationRequest()
            as AppointmentTypeCreatedNotificationRequest;

        // Assert
        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);
        Assert.Equal(_appointmentTypeId, notificationRequest?.Id);
        Assert.Equal(_appointmentTypeName, notificationRequest?.Name);
        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_WithoutEntries_ReturnsAppointmentTypeCreated()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.AppointmentTypeCreated.ToString(),
            notificationEvent
        );
    }
}