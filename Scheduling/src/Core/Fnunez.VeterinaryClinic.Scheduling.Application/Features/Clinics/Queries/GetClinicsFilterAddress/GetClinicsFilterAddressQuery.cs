using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterAddress;

public record GetClinicsFilterAddressQuery(GetClinicsFilterAddressRequest GetClinicsFilterAddressRequest)
    : IRequest<GetClinicsFilterAddressResponse>;