using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientDeleted;

public record ClientDeletedReceiveIntegrationEvent(ClientDeletedIntegrationEventContract ClientDeletedIntegrationEventContract)
    : INotification;