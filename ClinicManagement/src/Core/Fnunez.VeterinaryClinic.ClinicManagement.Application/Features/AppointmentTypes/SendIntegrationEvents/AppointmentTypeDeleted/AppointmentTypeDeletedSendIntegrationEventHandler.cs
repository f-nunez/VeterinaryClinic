using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeDeleted;

public class AppointmentTypeDeletedSendIntegrationEventHandler
    : INotificationHandler<AppointmentTypeDeletedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public AppointmentTypeDeletedSendIntegrationEventHandler(
        IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        AppointmentTypeDeletedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent
            .AppointmentTypeDeletedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}
