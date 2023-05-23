using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class PatientCreatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Patient _patient;

    public PatientCreatedIntegrationEventFactory(Patient patient)
    {
        _patient = patient;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new PatientCreatedIntegrationEvent
        {
            PatientBreed = _patient.AnimalType.Breed,
            PatientClientId = _patient.ClientId,
            PatientId = _patient.Id,
            PatientName = _patient.Name,
            PatientPhotoName = _patient.Photo.Name,
            PatientPhotoStoredName = _patient.Photo.StoredName,
            PatientPreferredDoctorId = _patient.PreferredDoctorId,
            PatientSex = (int)_patient.AnimalSex,
            PatientSpecies = _patient.AnimalType.Species
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.PatientCreated.ToString();
    }
}