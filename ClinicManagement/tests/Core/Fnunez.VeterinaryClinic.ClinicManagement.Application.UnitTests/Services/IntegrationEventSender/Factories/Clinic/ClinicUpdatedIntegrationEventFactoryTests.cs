using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class ClinicUpdatedIntegrationEventFactoryTests
{
    private readonly string _clinicAddress = "a";
    private readonly string _clinicEmailAddress = "test@test.com";
    private readonly int _clinicId = 1;
    private readonly string _clinicName = "a";
    private readonly ClinicUpdatedIntegrationEventFactory _factory;

    public ClinicUpdatedIntegrationEventFactoryTests()
    {
        var clinic = new Clinic(
            _clinicId,
            _clinicAddress,
            _clinicEmailAddress,
            _clinicName
        );

        _factory = new ClinicUpdatedIntegrationEventFactory(clinic);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsClinicUpdatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<ClinicUpdatedIntegrationEvent>(actual);

        var integrationEvent = actual as ClinicUpdatedIntegrationEvent;

        Assert.Equal(_clinicAddress, integrationEvent?.ClinicAddress);

        Assert.Equal(_clinicEmailAddress, integrationEvent?.ClinicEmailAddress);

        Assert.Equal(_clinicId, integrationEvent?.ClinicId);

        Assert.Equal(_clinicName, integrationEvent?.ClinicName);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsClinicUpdated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.ClinicUpdated.ToString(),
            integrationEvent
        );
    }
}