using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class ClientCreatedIntegrationEventFactoryTests
{
    private readonly string _clientEmailAddress = "test@test.com";
    private readonly string _clientFullName = "a";
    private readonly int _clientId = 1;
    private readonly int _clientPreferredDoctorId = 1;
    private readonly PreferredLanguage _clientPreferredLanguage = PreferredLanguage.English;
    private readonly string _clientPreferredName = "a";
    private readonly string _clientSalutation = "a";
    private readonly ClientCreatedIntegrationEventFactory _factory;

    public ClientCreatedIntegrationEventFactoryTests()
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

        _factory = new ClientCreatedIntegrationEventFactory(client);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsClientCreatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<ClientCreatedIntegrationEvent>(actual);

        var integrationEvent = actual as ClientCreatedIntegrationEvent;

        Assert.Equal(_clientEmailAddress, integrationEvent?.ClientEmailAddress);

        Assert.Equal(_clientFullName, integrationEvent?.ClientFullName);

        Assert.Equal(_clientId, integrationEvent?.ClientId);

        Assert.Equal(_clientPreferredDoctorId, integrationEvent?.ClientPreferredDoctorId);

        Assert.Equal((int)_clientPreferredLanguage, integrationEvent?.ClientPreferredLanguage);

        Assert.Equal(_clientPreferredName, integrationEvent?.ClientPreferredName);

        Assert.Equal(_clientSalutation, integrationEvent?.ClientSalutation);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsClientCreated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.ClientCreated.ToString(),
            integrationEvent
        );
    }
}