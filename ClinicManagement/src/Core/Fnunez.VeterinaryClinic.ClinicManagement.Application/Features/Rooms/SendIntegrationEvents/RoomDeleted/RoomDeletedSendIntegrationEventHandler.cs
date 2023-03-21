using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomDeleted;

public class RoomDeletedSendIntegrationEventHandler
    : INotificationHandler<RoomDeletedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public RoomDeletedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        RoomDeletedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.RoomDeletedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}