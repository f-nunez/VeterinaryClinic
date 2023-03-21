using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicDeleted;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class ClinicDeletedIntegrationEventConsumer
    : IConsumer<ClinicDeletedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public ClinicDeletedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<ClinicDeletedIntegrationEventContract> context)
    {
        var integrationEvent = new ClinicDeletedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}