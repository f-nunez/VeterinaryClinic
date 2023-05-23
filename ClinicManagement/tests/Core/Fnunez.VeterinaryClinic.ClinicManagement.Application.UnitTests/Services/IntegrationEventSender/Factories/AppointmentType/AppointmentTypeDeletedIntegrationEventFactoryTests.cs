using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender.Factories;

public class AppointmentTypeDeletedIntegrationEventFactoryTests
{
    private readonly string _appointmentTypeCode = "c";
    private readonly int _appointmentTypeDuration = 1;
    private readonly int _appointmentTypeId = 1;
    private readonly string _appointmentTypeName = "n";
    private readonly AppointmentTypeDeletedIntegrationEventFactory _factory;

    public AppointmentTypeDeletedIntegrationEventFactoryTests()
    {
        var appointmentType = new AppointmentType(
            _appointmentTypeId,
            _appointmentTypeName,
            _appointmentTypeCode,
            _appointmentTypeDuration
        );

        _factory = new AppointmentTypeDeletedIntegrationEventFactory(
            appointmentType
        );
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsAppointmentTypeDeletedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<AppointmentTypeDeletedIntegrationEvent>(actual);

        var integrationEvent = actual as AppointmentTypeDeletedIntegrationEvent;

        Assert.Equal(_appointmentTypeId, integrationEvent?.AppointmentTypeId);

    }

    [Fact]
    public void GetIntegrationEvent_ReturnsAppointmentTypeDeleted()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.AppointmentTypeDeleted.ToString(),
            integrationEvent
        );
    }
}