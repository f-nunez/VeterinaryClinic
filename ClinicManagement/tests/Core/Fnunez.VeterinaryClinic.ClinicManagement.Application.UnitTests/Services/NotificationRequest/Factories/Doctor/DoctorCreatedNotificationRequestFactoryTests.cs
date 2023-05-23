using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class DoctorCreatedNotificationRequestFactoryTests
{
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly string _doctorFullName = "a";
    private readonly int _doctorId = 1;
    private readonly DoctorCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public DoctorCreatedNotificationRequestFactoryTests()
    {
        var doctor = new Doctor(_doctorId, _doctorFullName);

        _factory = new DoctorCreatedNotificationRequestFactory
        (
            doctor,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsDoctorCreatedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<DoctorCreatedNotificationRequest>(actual);

        var notificationRequest = actual as DoctorCreatedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_doctorFullName, notificationRequest?.FullName);

        Assert.Equal(_doctorId, notificationRequest?.Id);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.DoctorCreated.ToString(),
            notificationEvent
        );
    }
}