using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientUpdated;

public record ClientUpdatedReceiveIntegrationEvent(ClientUpdatedIntegrationEventContract ClientUpdatedIntegrationEventContract)
    : INotification;