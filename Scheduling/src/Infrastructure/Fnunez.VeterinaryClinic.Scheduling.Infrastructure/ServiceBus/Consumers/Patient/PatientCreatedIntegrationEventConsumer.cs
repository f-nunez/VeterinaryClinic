using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientCreated;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class PatientCreatedIntegrationEventConsumer
    : IConsumer<PatientCreatedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public PatientCreatedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(
        ConsumeContext<PatientCreatedIntegrationEventContract> context)
    {
        var integrationEvent = new PatientCreatedReceiveIntegrationEvent(
            context.Message);

        await _mediator.Publish(integrationEvent);
    }
}