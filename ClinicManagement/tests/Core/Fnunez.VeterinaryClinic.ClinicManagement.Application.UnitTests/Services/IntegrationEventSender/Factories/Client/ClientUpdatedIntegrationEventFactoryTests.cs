using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class ClientUpdatedIntegrationEventFactoryTests
{
    private readonly string _clientEmailAddress = "test@test.com";
    private readonly string _clientFullName = "a";
    private readonly int _clientId = 1;
    private readonly int _clientPreferredDoctorId = 1;
    private readonly PreferredLanguage _clientPreferredLanguage = PreferredLanguage.English;
    private readonly string _clientPreferredName = "a";
    private readonly string _clientSalutation = "a";
    private readonly ClientUpdatedIntegrationEventFactory _factory;

    public ClientUpdatedIntegrationEventFactoryTests()
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

        _factory = new ClientUpdatedIntegrationEventFactory(client);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsClientUpdatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<ClientUpdatedIntegrationEvent>(actual);

        var integrationEvent = actual as ClientUpdatedIntegrationEvent;

        Assert.Equal(_clientEmailAddress, integrationEvent?.ClientEmailAddress);

        Assert.Equal(_clientFullName, integrationEvent?.ClientFullName);

        Assert.Equal(_clientId, integrationEvent?.ClientId);

        Assert.Equal(_clientPreferredDoctorId, integrationEvent?.ClientPreferredDoctorId);

        Assert.Equal((int)_clientPreferredLanguage, integrationEvent?.ClientPreferredLanguage);

        Assert.Equal(_clientPreferredName, integrationEvent?.ClientPreferredName);

        Assert.Equal(_clientSalutation, integrationEvent?.ClientSalutation);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsClientUpdated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.ClientUpdated.ToString(),
            integrationEvent
        );
    }
}