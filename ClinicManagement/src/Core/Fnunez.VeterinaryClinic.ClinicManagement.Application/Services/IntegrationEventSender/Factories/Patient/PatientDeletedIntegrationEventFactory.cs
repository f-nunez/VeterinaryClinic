using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class PatientDeletedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Patient _patient;

    public PatientDeletedIntegrationEventFactory(Patient patient)
    {
        _patient = patient;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new PatientDeletedIntegrationEvent
        {
            PatientClientId = _patient.ClientId,
            PatientId = _patient.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.PatientDeleted.ToString();
    }
}