using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.ServiceBus;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomUpdated;

public class RoomUpdatedSendIntegrationEventHandler
    : INotificationHandler<RoomUpdatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public RoomUpdatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public Task Handle(
        RoomUpdatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.RoomUpdatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}