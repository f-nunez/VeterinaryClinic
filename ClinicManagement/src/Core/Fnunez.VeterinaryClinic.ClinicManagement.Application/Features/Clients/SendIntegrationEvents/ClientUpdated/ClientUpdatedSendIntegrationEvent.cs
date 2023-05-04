using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientUpdated;

public record ClientUpdatedSendIntegrationEvent(ClientUpdatedIntegrationEventContract ClientUpdatedIntegrationEventContract)
    : INotification;