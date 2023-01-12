using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterId;

public record GetClientsFilterIdQuery(GetClientsFilterIdRequest GetClientsFilterIdRequest)
    : IRequest<GetClientsFilterIdResponse>;