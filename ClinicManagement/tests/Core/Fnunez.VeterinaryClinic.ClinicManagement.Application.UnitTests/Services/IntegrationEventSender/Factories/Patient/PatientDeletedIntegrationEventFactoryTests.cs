using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class PatientDeletedIntegrationEventFactoryTests
{
    private readonly PatientDeletedIntegrationEventFactory _factory;
    private readonly AnimalSex _patientAnimalSex = AnimalSex.Female;
    private readonly AnimalType _patientAnimalType = new AnimalType("a", "a");
    private readonly int _patientClientId = 1;
    private readonly int _patientId = 1;
    private readonly string _patientName = "a";
    private readonly Photo _patientPhoto = new Photo("a", "a");
    private readonly int? _patientPreferredDoctorId = 1;

    public PatientDeletedIntegrationEventFactoryTests()
    {
        var patient = new Patient(
            _patientId,
            _patientClientId,
            _patientName,
            _patientAnimalSex,
            _patientAnimalType,
            _patientPhoto,
            _patientPreferredDoctorId
        );

        _factory = new PatientDeletedIntegrationEventFactory(patient);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsPatientDeletedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<PatientDeletedIntegrationEvent>(actual);

        var integrationEvent = actual as PatientDeletedIntegrationEvent;

        Assert.Equal(_patientClientId, integrationEvent?.PatientClientId);

        Assert.Equal(_patientId, integrationEvent?.PatientId);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsPatientDeleted()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.PatientDeleted.ToString(),
            integrationEvent
        );
    }
}