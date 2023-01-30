using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.ReceiveIntegrationEvents.ClinicCreated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class ClinicCreatedIntegrationEventConsumer
    : IConsumer<ClinicCreatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public ClinicCreatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<ClinicCreatedIntegrationEventContract> context)
    {
        var integrationEvent = new ClinicCreatedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}