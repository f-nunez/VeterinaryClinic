using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class ClinicDeletedIntegrationEventFactoryTests
{
    private readonly string _clinicAddress = "a";
    private readonly string _clinicEmailAddress = "test@test.com";
    private readonly int _clinicId = 1;
    private readonly string _clinicName = "a";
    private readonly ClinicDeletedIntegrationEventFactory _factory;

    public ClinicDeletedIntegrationEventFactoryTests()
    {
        var clinic = new Clinic(
            _clinicId,
            _clinicAddress,
            _clinicEmailAddress,
            _clinicName
        );

        _factory = new ClinicDeletedIntegrationEventFactory(clinic);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsClinicDeletedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<ClinicDeletedIntegrationEvent>(actual);

        var integrationEvent = actual as ClinicDeletedIntegrationEvent;

        Assert.Equal(_clinicId, integrationEvent?.ClinicId);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsClinicDeleted()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.ClinicDeleted.ToString(),
            integrationEvent
        );
    }
}