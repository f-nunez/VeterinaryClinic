using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicDeleted;

public record ClinicDeletedSendIntegrationEvent(ClinicDeletedIntegrationEventContract ClinicDeletedIntegrationEventContract)
    : INotification;