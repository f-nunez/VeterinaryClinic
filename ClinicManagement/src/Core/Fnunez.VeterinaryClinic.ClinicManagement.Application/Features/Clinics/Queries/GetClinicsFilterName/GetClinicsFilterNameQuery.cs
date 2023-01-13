using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterName;

public record GetClinicsFilterNameQuery(GetClinicsFilterNameRequest GetClinicsFilterNameRequest)
    : IRequest<GetClinicsFilterNameResponse>;