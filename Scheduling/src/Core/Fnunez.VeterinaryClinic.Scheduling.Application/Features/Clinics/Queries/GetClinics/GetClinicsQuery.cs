using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinics;

public record GetClinicsQuery(GetClinicsRequest GetClinicsRequest)
    : IRequest<GetClinicsResponse>;