using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientCreated;

public class ClientCreatedSendIntegrationEventHandler
    : INotificationHandler<ClientCreatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public ClientCreatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        ClientCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClientCreatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}