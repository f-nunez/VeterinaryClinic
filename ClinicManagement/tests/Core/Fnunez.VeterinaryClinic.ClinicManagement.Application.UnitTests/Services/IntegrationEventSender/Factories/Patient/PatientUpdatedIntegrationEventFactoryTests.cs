using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.NotificationRequest.Factories;

public class PatientUpdatedIntegrationEventFactoryTests
{
    private readonly PatientUpdatedIntegrationEventFactory _factory;
    private readonly AnimalSex _patientAnimalSex = AnimalSex.Female;
    private readonly AnimalType _patientAnimalType = new AnimalType("a", "a");
    private readonly int _patientClientId = 1;
    private readonly int _patientId = 1;
    private readonly string _patientName = "a";
    private readonly Photo _patientPhoto = new Photo("a", "a");
    private readonly int? _patientPreferredDoctorId = 1;

    public PatientUpdatedIntegrationEventFactoryTests()
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

        _factory = new PatientUpdatedIntegrationEventFactory(patient);
    }

    [Fact]
    public void CreateIntegrationEvent_ReturnsPatientUpdatedIntegrationEvent()
    {
        // Act
        var actual = _factory.CreateIntegrationEvent();

        // Assert
        Assert.IsType<PatientUpdatedIntegrationEvent>(actual);

        var integrationEvent = actual as PatientUpdatedIntegrationEvent;

        Assert.Equal(_patientAnimalType.Breed, integrationEvent?.PatientBreed);

        Assert.Equal(_patientClientId, integrationEvent?.PatientClientId);

        Assert.Equal(_patientId, integrationEvent?.PatientId);

        Assert.Equal(_patientName, integrationEvent?.PatientName);

        Assert.Equal(_patientPhoto.Name, integrationEvent?.PatientPhotoName);

        Assert.Equal(_patientPhoto.StoredName, integrationEvent?.PatientPhotoStoredName);

        Assert.Equal(_patientPreferredDoctorId, integrationEvent?.PatientPreferredDoctorId);

        Assert.Equal((int)_patientAnimalSex, integrationEvent?.PatientSex);

        Assert.Equal(_patientAnimalType.Species, integrationEvent?.PatientSpecies);
    }

    [Fact]
    public void GetIntegrationEvent_ReturnsPatientUpdated()
    {
        // Act
        var integrationEvent = _factory.GetIntegrationEvent();

        // Assert
        Assert.Equal(
            IntegrationEvent.PatientUpdated.ToString(),
            integrationEvent
        );
    }
}