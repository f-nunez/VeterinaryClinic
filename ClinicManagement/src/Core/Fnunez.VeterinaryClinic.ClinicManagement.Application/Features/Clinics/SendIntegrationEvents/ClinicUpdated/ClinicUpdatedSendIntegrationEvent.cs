using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicUpdated;

public record ClinicUpdatedSendIntegrationEvent(ClinicUpdatedIntegrationEventContract ClinicUpdatedIntegrationEventContract)
    : INotification;