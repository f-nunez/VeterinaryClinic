using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public record GetClientsFilterPreferredNameQuery(GetClientsFilterPreferredNameRequest GetClientsFilterPreferredNameRequest)
    : IRequest<GetClientsFilterPreferredNameResponse>;