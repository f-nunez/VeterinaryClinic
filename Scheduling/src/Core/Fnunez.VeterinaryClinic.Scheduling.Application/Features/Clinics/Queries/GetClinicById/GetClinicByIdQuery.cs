using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicById;

public record GetClinicByIdQuery(GetClinicByIdRequest GetClinicByIdRequest)
    : IRequest<GetClinicByIdResponse>;