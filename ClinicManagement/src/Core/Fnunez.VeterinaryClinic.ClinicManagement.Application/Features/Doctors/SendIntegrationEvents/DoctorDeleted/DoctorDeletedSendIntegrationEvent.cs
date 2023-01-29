using Contracts;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.SendIntegrationEvents.DoctorDeleted;

public record DoctorDeletedSendIntegrationEvent(DoctorDeletedIntegrationEventContract DoctorDeletedIntegrationEventContract)
    : INotification;