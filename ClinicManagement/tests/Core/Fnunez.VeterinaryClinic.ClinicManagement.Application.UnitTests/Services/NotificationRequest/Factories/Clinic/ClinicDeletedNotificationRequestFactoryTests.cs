using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class ClinicDeletedNotificationRequestFactoryTests
{
    private readonly string _clinicAddress = "a";
    private readonly string _clinicEmailAddress = "test@test.com";
    private readonly int _clinicId = 1;
    private readonly string _clinicName = "a";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly ClinicDeletedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public ClinicDeletedNotificationRequestFactoryTests()
    {
        var clinic = new Clinic
        (
            _clinicId,
            _clinicAddress,
            _clinicEmailAddress,
            _clinicName
        );

        _factory = new ClinicDeletedNotificationRequestFactory
        (
            clinic,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsClinicDeletedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<ClinicDeletedNotificationRequest>(actual);

        var notificationRequest = actual as ClinicDeletedNotificationRequest;

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
            NotificationEvent.ClinicDeleted.ToString(),
            notificationEvent
        );
    }
}