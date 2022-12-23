using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterName;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterName;

public record GetClinicsFilterNameQuery(GetClinicsFilterNameRequest GetClinicsFilterNameRequest)
    : IRequest<GetClinicsFilterNameResponse>;