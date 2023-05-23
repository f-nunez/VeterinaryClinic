using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.Factories;

public interface IIntegrationEventFactory
{
    BaseIntegrationEvent GetDeserializedIntegrationEvent(IntegrationEvent integrationEvent, string serializedIntegrationEvent);
    object GetReceiveIntegrationEvent(IntegrationEvent integrationEvent, BaseIntegrationEvent baseIntegrationEvent);
}