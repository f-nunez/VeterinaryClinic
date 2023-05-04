using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.ReceiveIntegrationEvents.ClientCreated;

public record ClientCreatedReceiveIntegrationEvent(ClientCreatedIntegrationEventContract ClientCreatedIntegrationEventContract)
    : INotification;