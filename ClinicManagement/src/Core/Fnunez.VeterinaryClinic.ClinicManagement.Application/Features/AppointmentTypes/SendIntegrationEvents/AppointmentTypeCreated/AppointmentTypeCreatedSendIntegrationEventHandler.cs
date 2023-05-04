using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeCreated;

public class AppointmentTypeCreatedSendIntegrationEventHandler
    : INotificationHandler<AppointmentTypeCreatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public AppointmentTypeCreatedSendIntegrationEventHandler(
        IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        AppointmentTypeCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent
            .AppointmentTypeCreatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}