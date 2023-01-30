using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.SendIntegrationEvents.RoomCreated;

public record RoomCreatedSendIntegrationEvent(RoomCreatedIntegrationEventContract RoomCreatedIntegrationEventContract)
    : INotification;