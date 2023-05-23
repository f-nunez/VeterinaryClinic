using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class PatientUpdatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Patient _patient;

    public PatientUpdatedIntegrationEventFactory(Patient patient)
    {
        _patient = patient;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new PatientUpdatedIntegrationEvent
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
        return IntegrationEvent.PatientUpdated.ToString();
    }
}