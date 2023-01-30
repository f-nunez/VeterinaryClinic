using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.ServiceBus;
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

    public Task Handle(
        RoomCreatedSendIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var message = integrationEvent.RoomCreatedIntegrationEventContract;

        return _serviceBus.PublishAsync(message, cancellationToken);
    }
}