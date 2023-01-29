using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomDeleted;

public record RoomDeletedSendIntegrationEvent(RoomDeletedIntegrationEventContract RoomDeletedIntegrationEventContract)
    : INotification;