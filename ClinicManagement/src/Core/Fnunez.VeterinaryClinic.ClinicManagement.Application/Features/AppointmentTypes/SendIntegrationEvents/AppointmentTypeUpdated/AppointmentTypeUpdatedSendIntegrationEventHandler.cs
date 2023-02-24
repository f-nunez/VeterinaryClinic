using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeUpdated;

public class AppointmentTypeUpdatedSendIntegrationEventHandler
    : INotificationHandler<AppointmentTypeUpdatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public AppointmentTypeUpdatedSendIntegrationEventHandler(
        IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(AppointmentTypeUpdatedSendIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var message = integrationEvent
            .AppointmentTypeUpdatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}
