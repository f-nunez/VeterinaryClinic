using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class ClinicCreatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Clinic _clinic;

    public ClinicCreatedIntegrationEventFactory(Clinic clinic)
    {
        _clinic = clinic;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new ClinicCreatedIntegrationEvent
        {
            ClinicAddress = _clinic.Address,
            ClinicEmailAddress = _clinic.EmailAddress,
            ClinicId = _clinic.Id,
            ClinicName = _clinic.Name
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.ClinicCreated.ToString();
    }
}