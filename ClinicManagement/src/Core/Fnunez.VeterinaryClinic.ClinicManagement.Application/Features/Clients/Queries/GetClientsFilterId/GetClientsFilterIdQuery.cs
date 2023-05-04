using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;

public record GetClientsFilterIdQuery(GetClientsFilterIdRequest GetClientsFilterIdRequest)
    : IRequest<GetClientsFilterIdResponse>;