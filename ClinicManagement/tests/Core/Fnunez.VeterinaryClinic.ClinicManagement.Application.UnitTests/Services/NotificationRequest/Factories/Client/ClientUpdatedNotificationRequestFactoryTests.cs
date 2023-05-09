using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class ClientUpdatedNotificationRequestFactoryTests
{
    private readonly string _clientEmailAddress = "test@test.com";
    private readonly string _clientFullName = "a";
    private readonly int _clientId = 1;
    private readonly int _clientPreferredDoctorId = 1;
    private readonly PreferredLanguage _clientPreferredLanguage = PreferredLanguage.English;
    private readonly string _clientPreferredName = "a";
    private readonly string _clientSalutation = "a";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly ClientUpdatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public ClientUpdatedNotificationRequestFactoryTests()
    {
        var client = new Client(
            _clientId,
            _clientFullName,
            _clientPreferredName,
            _clientSalutation,
            _clientEmailAddress,
            _clientPreferredLanguage,
            _clientPreferredDoctorId
        );

        _factory = new ClientUpdatedNotificationRequestFactory(
            client,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_WithoutEntries_ReturnsNotificationRequest()
    {
        // Act
        var notificationRequest = _factory.CreateNotificationRequest()
            as ClientUpdatedNotificationRequest;

        // Assert
        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);
        Assert.Equal(_clientFullName, notificationRequest?.FullName);
        Assert.Equal(_clientId, notificationRequest?.Id);
        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_WithoutEntries_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.ClientUpdated.ToString(),
            notificationEvent
        );
    }
}