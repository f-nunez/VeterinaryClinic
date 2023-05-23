using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public interface IIntegrationEventFactory
{
    BaseIntegrationEvent CreateIntegrationEvent();
    string GetIntegrationEvent();
}