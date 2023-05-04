using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;

public record DeleteClinicCommand(DeleteClinicRequest DeleteClinicRequest)
    : IRequest<DeleteClinicResponse>;