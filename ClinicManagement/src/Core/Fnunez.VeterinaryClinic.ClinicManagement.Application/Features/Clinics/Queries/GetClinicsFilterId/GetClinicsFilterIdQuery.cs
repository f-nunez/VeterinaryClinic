using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterId;

public record GetClinicsFilterIdQuery(GetClinicsFilterIdRequest GetClinicsFilterIdRequest)
    : IRequest<GetClinicsFilterIdResponse>;