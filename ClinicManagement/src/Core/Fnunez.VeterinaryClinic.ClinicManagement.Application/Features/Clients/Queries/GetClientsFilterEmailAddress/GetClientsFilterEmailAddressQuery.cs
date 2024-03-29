using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;

public record GetClientsFilterEmailAddressQuery(GetClientsFilterEmailAddressRequest GetClientsFilterEmailAddressRequest)
    : IRequest<GetClientsFilterEmailAddressResponse>;