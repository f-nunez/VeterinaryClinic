using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorCreated;

public record DoctorCreatedSendIntegrationEvent(DoctorCreatedIntegrationEventContract DoctorCreatedIntegrationEventContract)
    : INotification;