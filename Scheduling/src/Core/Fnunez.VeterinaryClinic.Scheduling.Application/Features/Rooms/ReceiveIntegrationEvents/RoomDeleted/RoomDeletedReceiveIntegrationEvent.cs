using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomDeleted;

public record RoomDeletedReceiveIntegrationEvent(RoomDeletedIntegrationEvent RoomDeletedIntegrationEvent)
    : INotification;