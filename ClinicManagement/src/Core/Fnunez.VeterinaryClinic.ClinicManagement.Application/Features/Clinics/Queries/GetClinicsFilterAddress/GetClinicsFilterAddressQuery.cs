using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterAddress;

public record GetClinicsFilterAddressQuery(GetClinicsFilterAddressRequest GetClinicsFilterAddressRequest)
    : IRequest<GetClinicsFilterAddressResponse>;