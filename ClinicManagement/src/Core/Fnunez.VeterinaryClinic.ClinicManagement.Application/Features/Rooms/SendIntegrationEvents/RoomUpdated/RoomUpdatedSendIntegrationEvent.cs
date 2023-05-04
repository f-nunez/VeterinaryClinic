using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomUpdated;

public record RoomUpdatedSendIntegrationEvent(RoomUpdatedIntegrationEventContract RoomUpdatedIntegrationEventContract)
    : INotification;