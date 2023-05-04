using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.CreateClinic;

public record CreateClinicCommand(CreateClinicRequest CreateClinicRequest)
    : IRequest<CreateClinicResponse>;