using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class ClinicDeletedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Clinic _clinic;

    public ClinicDeletedIntegrationEventFactory(Clinic clinic)
    {
        _clinic = clinic;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new ClinicDeletedIntegrationEvent
        {
            ClinicId = _clinic.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.ClinicDeleted.ToString();
    }
}