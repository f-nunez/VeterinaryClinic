using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicDeleted;

public class ClinicDeletedSendIntegrationEventHandler
    : INotificationHandler<ClinicDeletedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public ClinicDeletedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        ClinicDeletedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClinicDeletedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}