using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class DoctorCreatedIntegrationEventFactoryTests
{
    private readonly string _doctorFullName = "a";
    private readonly int _doctorId = 1;
    private readonly DoctorCreatedIntegrationEventFactory _factory;

    public DoctorCreatedIntegrationEventFactoryTests()
    {
        var doctor = new Doctor(_doctorId, _doctorFullName);

        _factory = new DoctorCreatedIntegrationEventFactory(doctor);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsDoctorCreatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<DoctorCreatedIntegrationEvent>(actual);

        var integrationEvent = actual as DoctorCreatedIntegrationEvent;

        Assert.Equal(_doctorFullName, integrationEvent?.DoctorFullName);

        Assert.Equal(_doctorId, integrationEvent?.DoctorId);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsDoctorCreated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.DoctorCreated.ToString(),
            integrationEvent
        );
    }
}