using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.IntegrationEvents;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;

public class ClientCreatedIntegrationEventFactory
    : IIntegrationEventFactory
{
    private readonly Client _client;

    public ClientCreatedIntegrationEventFactory(Client client)
    {
        _client = client;
    }

    public BaseIntegrationEvent CreateIntegrationEvent()
    {
        return new ClientCreatedIntegrationEvent
        {
            ClientEmailAddress = _client.EmailAddress,
            ClientFullName = _client.FullName,
            ClientId = _client.Id,
            ClientPreferredDoctorId = _client.PreferredDoctorId,
            ClientPreferredLanguage = (int)_client.PreferredLanguage,
            ClientPreferredName = _client.PreferredName,
            ClientSalutation = _client.Salutation
        };
    }

    public string GetIntegrationEvent()
    {
        return IntegrationEvent.ClientCreated.ToString();
    }
}