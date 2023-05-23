using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomUpdated;

public record RoomUpdatedReceiveIntegrationEvent(RoomUpdatedIntegrationEvent RoomUpdatedIntegrationEvent)
    : INotification;