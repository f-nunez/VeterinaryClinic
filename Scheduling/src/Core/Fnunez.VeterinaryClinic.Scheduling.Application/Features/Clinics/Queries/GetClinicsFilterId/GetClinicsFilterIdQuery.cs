using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterId;

public record GetClinicsFilterIdQuery(GetClinicsFilterIdRequest GetClinicsFilterIdRequest)
    : IRequest<GetClinicsFilterIdResponse>;