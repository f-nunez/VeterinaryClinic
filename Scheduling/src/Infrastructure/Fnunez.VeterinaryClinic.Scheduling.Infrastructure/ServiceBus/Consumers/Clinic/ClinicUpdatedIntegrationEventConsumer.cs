using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicUpdated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class ClinicUpdatedIntegrationEventConsumer
    : IConsumer<ClinicUpdatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public ClinicUpdatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<ClinicUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new ClinicUpdatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}