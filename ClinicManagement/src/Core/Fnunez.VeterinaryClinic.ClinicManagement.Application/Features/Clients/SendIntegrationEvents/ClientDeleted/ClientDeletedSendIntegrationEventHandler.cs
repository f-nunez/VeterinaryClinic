using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientDeleted;

public class ClientDeletedSendIntegrationEventHandler
    : INotificationHandler<ClientDeletedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public ClientDeletedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        ClientDeletedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClientDeletedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}