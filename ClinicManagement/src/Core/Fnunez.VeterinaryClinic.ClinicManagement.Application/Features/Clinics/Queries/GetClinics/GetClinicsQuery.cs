using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinics;

public record GetClinicsQuery(GetClinicsRequest GetClinicsRequest)
    : IRequest<GetClinicsResponse>;