using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientUpdated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class PatientUpdatedIntegrationEventConsumer
    : IConsumer<PatientUpdatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public PatientUpdatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<PatientUpdatedIntegrationEventContract> context)
    {
        var integrationEvent = new PatientUpdatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent, context.CancellationToken);
    }
}