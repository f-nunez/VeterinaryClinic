using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;

public record GetClientsQuery(GetClientsRequest GetClientsRequest)
    : IRequest<GetClientsResponse>;