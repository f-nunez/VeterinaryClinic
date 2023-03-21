using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicCreated;

public class ClinicCreatedSendIntegrationEventHandler
    : INotificationHandler<ClinicCreatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public ClinicCreatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        ClinicCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.ClinicCreatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}