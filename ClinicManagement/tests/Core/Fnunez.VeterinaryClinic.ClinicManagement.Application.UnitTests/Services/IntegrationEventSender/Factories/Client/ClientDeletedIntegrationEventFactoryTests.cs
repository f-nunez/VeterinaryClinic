using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class ClientDeletedIntegrationEventFactoryTests
{
    private readonly string _clientEmailAddress = "test@test.com";
    private readonly string _clientFullName = "a";
    private readonly int _clientId = 1;
    private readonly int _clientPreferredDoctorId = 1;
    private readonly PreferredLanguage _clientPreferredLanguage = PreferredLanguage.English;
    private readonly string _clientPreferredName = "a";
    private readonly string _clientSalutation = "a";
    private readonly ClientDeletedIntegrationEventFactory _factory;

    public ClientDeletedIntegrationEventFactoryTests()
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

        _factory = new ClientDeletedIntegrationEventFactory(client);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsClientDeletedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<ClientDeletedIntegrationEvent>(actual);

        var integrationEvent = actual as ClientDeletedIntegrationEvent;

        Assert.Equal(_clientId, integrationEvent?.ClientId);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsClientDeleted()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.ClientDeleted.ToString(),
            integrationEvent
        );
    }
}