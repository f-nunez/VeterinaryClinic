using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class AppointmentTypeUpdatedIntegrationEventFactoryTests
{
    private readonly string _appointmentTypeCode = "c";
    private readonly int _appointmentTypeDuration = 1;
    private readonly int _appointmentTypeId = 1;
    private readonly string _appointmentTypeName = "n";
    private readonly AppointmentTypeUpdatedIntegrationEventFactory _factory;

    public AppointmentTypeUpdatedIntegrationEventFactoryTests()
    {
        var appointmentType = new AppointmentType(
            _appointmentTypeId,
            _appointmentTypeName,
            _appointmentTypeCode,
            _appointmentTypeDuration
        );

        _factory = new AppointmentTypeUpdatedIntegrationEventFactory(
            appointmentType
        );
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsAppointmentTypeUpdatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<AppointmentTypeUpdatedIntegrationEvent>(actual);

        var integrationEvent = actual as AppointmentTypeUpdatedIntegrationEvent;

        Assert.Equal(_appointmentTypeCode, integrationEvent?.AppointmentTypeCode);

        Assert.Equal(_appointmentTypeDuration, integrationEvent?.AppointmentTypeDuration);

        Assert.Equal(_appointmentTypeId, integrationEvent?.AppointmentTypeId);

        Assert.Equal(_appointmentTypeName, integrationEvent?.AppointmentTypeName);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsAppointmentTypeUpdated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.AppointmentTypeUpdated.ToString(),
            integrationEvent
        );
    }
}