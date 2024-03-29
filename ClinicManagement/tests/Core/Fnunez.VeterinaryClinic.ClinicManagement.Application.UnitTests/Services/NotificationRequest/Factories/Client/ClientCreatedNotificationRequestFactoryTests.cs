using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class ClientCreatedNotificationRequestFactoryTests
{
    private readonly string _clientEmailAddress = "test@test.com";
    private readonly string _clientFullName = "a";
    private readonly int _clientId = 1;
    private readonly int _clientPreferredDoctorId = 1;
    private readonly PreferredLanguage _clientPreferredLanguage = PreferredLanguage.English;
    private readonly string _clientPreferredName = "a";
    private readonly string _clientSalutation = "a";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly ClientCreatedNotificationRequestFactory _factory;
    private readonly string _userId = Guid.NewGuid().ToString();

    public ClientCreatedNotificationRequestFactoryTests()
    {
        var client = new Client
        (
            _clientId,
            _clientFullName,
            _clientPreferredName,
            _clientSalutation,
            _clientEmailAddress,
            _clientPreferredLanguage,
            _clientPreferredDoctorId
        );

        _factory = new ClientCreatedNotificationRequestFactory
        (
            client,
            _correlationId,
            _userId
        );
    }

    [Fact]
    public void CreateNotificationRequest_ReturnsClientCreatedNotificationRequest()
    {
        // Act
        var actual = _factory.CreateNotificationRequest();

        // Assert
        Assert.IsType<ClientCreatedNotificationRequest>(actual);

        var notificationRequest = actual as ClientCreatedNotificationRequest;

        Assert.Equal(_correlationId, notificationRequest?.CorrelationId);

        Assert.Equal(_clientFullName, notificationRequest?.FullName);

        Assert.Equal(_clientId, notificationRequest?.Id);

        Assert.Equal(_userId, notificationRequest?.TriggeredByUserId);
    }

    [Fact]
    public void GetNotificationEvent_ReturnsNotificationEvent()
    {
        // Act
        var notificationEvent = _factory.GetNotificationEvent();

        // Assert
        Assert.Equal(
            NotificationEvent.ClientCreated.ToString(),
            notificationEvent
        );
    }
}