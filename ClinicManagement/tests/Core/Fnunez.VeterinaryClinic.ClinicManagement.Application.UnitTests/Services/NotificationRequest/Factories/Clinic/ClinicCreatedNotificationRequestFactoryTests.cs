using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class ClinicCreatedNotificationRequestFactoryTests
{
    private readonly string _clinicAddress = "a";
    private readonly string _clinicEmailAddress = "test@test.com";
    private readonly int _clinicId = 1;
    private readonly string _clinicName = "a";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly ClinicCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public ClinicCreatedNotificationRequestFactoryTests()
    {
        var clinic = new Clinic
        (
            _clinicId,
            _clinicAddress,
            _clinicEmailAddress,
            _clinicName
        );

        _factory = new ClinicCreatedNotificationRequestFactory
        (
            clinic,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsClinicCreatedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<ClinicCreatedNotificationRequest>(actual);

        var notificationRequest = actual as ClinicCreatedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_clinicId, notificationRequest?.Id);

        Assert.Equal(_clinicName, notificationRequest?.Name);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.ClinicCreated.ToString(),
            notificationEvent
        );
    }
}