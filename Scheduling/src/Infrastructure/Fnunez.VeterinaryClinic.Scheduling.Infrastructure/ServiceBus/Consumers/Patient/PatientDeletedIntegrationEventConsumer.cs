using Contracts;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.ReceiveIntegrationEvents.PatientDeleted;
using MassTransit;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus.Consumers;

public class PatientDeletedIntegrationEventConsumer
    : IConsumer<PatientDeletedIntegrationEventContract>
{
    private readonly IMediator _mediator;

    public PatientDeletedIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(
        ConsumeContext<PatientDeletedIntegrationEventContract> context)
    {
        var integrationEvent = new PatientDeletedReceiveIntegrationEvent(
            context.Message);

        return _mediator.Publish(integrationEvent);
    }
}