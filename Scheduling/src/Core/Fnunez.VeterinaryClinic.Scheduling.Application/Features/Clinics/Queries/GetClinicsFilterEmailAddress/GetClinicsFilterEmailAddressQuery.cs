using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;

public record GetClinicsFilterEmailAddressQuery(GetClinicsFilterEmailAddressRequest GetClinicsFilterEmailAddressRequest)
    : IRequest<GetClinicsFilterEmailAddressResponse>;