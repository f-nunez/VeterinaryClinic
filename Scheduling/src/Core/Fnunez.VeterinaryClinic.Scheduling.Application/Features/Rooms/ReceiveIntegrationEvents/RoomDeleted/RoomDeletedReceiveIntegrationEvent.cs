using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomDeleted;

public record RoomDeletedReceiveIntegrationEvent(RoomDeletedIntegrationEventContract RoomDeletedIntegrationEventContract)
    : INotification;