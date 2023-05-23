using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class ClientDeletedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Client _client;

    public ClientDeletedIntegrationEventFactory(Client client)
    {
        _client = client;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new ClientDeletedIntegrationEvent
        {
            ClientId = _client.Id
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.ClientDeleted.ToString();
    }
}