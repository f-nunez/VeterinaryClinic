using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomCreated;

public class RoomCreatedSendIntegrationEventHandler
    : INotificationHandler<RoomCreatedSendIntegrationEvent>
{
    private readonly IServiceBus _serviceBus;

    public RoomCreatedSendIntegrationEventHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task Handle(
        RoomCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.RoomCreatedIntegrationEventContract;

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}