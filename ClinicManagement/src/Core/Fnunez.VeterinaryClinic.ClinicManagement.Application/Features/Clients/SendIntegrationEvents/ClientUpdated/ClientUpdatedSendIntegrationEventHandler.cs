using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.ServiceBus;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientUpdated;

public class ClientUpdatedSendIntegrationEventHandler
    : INotificationHandler<ClientUpdatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public ClientUpdatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        ClientUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClientUpdatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}