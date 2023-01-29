using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.SendIntegrationEvents.ClinicCreated;

public record ClinicCreatedSendIntegrationEvent(ClinicCreatedIntegrationEventContract ClinicCreatedIntegrationEventContract)
    : INotification;