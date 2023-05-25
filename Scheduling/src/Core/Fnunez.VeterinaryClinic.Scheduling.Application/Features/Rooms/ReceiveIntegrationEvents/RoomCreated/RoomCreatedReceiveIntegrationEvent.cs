using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomCreated;

public record RoomCreatedReceiveIntegrationEvent(RoomCreatedIntegrationEvent RoomCreatedIntegrationEvent)
    : INotification;