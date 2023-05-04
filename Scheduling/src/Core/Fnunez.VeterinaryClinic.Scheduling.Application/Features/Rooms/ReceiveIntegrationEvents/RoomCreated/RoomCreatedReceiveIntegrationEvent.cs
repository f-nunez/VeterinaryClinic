using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.ReceiveIntegrationEvents.RoomCreated;

public record RoomCreatedReceiveIntegrationEvent(RoomCreatedIntegrationEventContract RoomCreatedIntegrationEventContract)
    : INotification;