using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class DoctorDeletedIntegrationEventFactoryTests
{
    private readonly string _doctorFullName = "a";
    private readonly int _doctorId = 1;
    private readonly DoctorDeletedIntegrationEventFactory _factory;

    public DoctorDeletedIntegrationEventFactoryTests()
    {
        var doctor = new Doctor(_doctorId, _doctorFullName);

        _factory = new DoctorDeletedIntegrationEventFactory(doctor);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsDoctorDeletedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<DoctorDeletedIntegrationEvent>(actual);

        var integrationEvent = actual as DoctorDeletedIntegrationEvent;

        Assert.Equal(_doctorId, integrationEvent?.DoctorId);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsDoctorDeleted()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.DoctorDeleted.ToString(),
            integrationEvent
        );
    }
}