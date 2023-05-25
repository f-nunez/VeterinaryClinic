using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.Factories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver;

public class IntegrationEventReceiverService : IIntegrationEventReceiverService
{
    private readonly IIntegrationEventFactory _integrationEventFactory;
    private readonly IMediator _mediator;

    public IntegrationEventReceiverService(
        IIntegrationEventFactory integrationEventFactory,
        IMediator mediator)
    {
        _integrationEventFactory = integrationEventFactory;
        _mediator = mediator;
    }

    public async Task ReceiveAsync(
        string integrationEventString,
        string serializedIntegrationEvent,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(integrationEventString))
            throw new ArgumentException(
                $"{nameof(integrationEventString)} is empty.");

        if (string.IsNullOrEmpty(serializedIntegrationEvent))
            throw new ArgumentException(
                $"{nameof(serializedIntegrationEvent)} is empty.");

        IntegrationEvent integrationEvent = GetIntegrationEvent(integrationEventString);

        var deserializedIntegrationEvent = _integrationEventFactory
            .GetDeserializedIntegrationEvent(integrationEvent, serializedIntegrationEvent);

        var receiveIntegrationEvent = _integrationEventFactory
            .GetReceiveIntegrationEvent(integrationEvent, deserializedIntegrationEvent);

        await _mediator.Publish(receiveIntegrationEvent, cancellationToken);
    }

    private IntegrationEvent GetIntegrationEvent(string integrationEventString)
    {
        bool isParsedIntegrationEvent = Enum.TryParse(
            integrationEventString, out IntegrationEvent integrationEvent);

        if (isParsedIntegrationEvent)
            return integrationEvent;

        throw new ArgumentException(
            $"{nameof(integrationEventString)} not found with value: {integrationEventString}");
    }
}