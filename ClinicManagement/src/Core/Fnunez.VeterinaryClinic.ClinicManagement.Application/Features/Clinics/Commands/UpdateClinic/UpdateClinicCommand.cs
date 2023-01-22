using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;

public record UpdateClinicCommand(UpdateClinicRequest UpdateClinicRequest)
    : IRequest<UpdateClinicResponse>;