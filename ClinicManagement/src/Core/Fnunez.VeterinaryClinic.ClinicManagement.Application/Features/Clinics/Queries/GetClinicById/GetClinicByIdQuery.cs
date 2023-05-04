using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicById;

public record GetClinicByIdQuery(GetClinicByIdRequest GetClinicByIdRequest)
    : IRequest<GetClinicByIdResponse>;