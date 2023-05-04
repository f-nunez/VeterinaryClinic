using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientCreated;

public record ClientCreatedSendIntegrationEvent(ClientCreatedIntegrationEventContract ClientCreatedIntegrationEventContract)
    : INotification;