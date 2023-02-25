using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
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

    public async Task Handle(
        ClientUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClientUpdatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}