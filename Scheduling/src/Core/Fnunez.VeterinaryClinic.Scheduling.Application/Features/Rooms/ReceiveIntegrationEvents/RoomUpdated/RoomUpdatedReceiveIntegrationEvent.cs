using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomUpdated;

public record RoomUpdatedReceiveIntegrationEvent(RoomUpdatedIntegrationEventContract RoomUpdatedIntegrationEventContract)
    : INotification;