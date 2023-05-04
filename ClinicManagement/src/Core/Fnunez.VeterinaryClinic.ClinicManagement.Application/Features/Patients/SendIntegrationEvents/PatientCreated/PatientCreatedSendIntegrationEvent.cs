using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientCreated;

public record PatientCreatedSendIntegrationEvent(PatientCreatedIntegrationEventContract PatientCreatedIntegrationEventContract)
    : INotification;