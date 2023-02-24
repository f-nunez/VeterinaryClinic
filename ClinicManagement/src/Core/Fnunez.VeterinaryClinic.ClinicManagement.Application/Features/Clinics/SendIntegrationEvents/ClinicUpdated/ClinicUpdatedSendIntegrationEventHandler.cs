using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicUpdated;

public class ClinicUpdatedSendIntegrationEventHandler
    : INotificationHandler<ClinicUpdatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public ClinicUpdatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        ClinicUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClinicUpdatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}