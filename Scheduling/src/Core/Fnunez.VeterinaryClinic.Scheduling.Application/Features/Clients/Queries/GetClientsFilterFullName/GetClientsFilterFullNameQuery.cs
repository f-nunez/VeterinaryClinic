using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterFullName;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterFullName;

public record GetClientsFilterFullNameQuery(GetClientsFilterFullNameRequest GetClientsFilterFullNameRequest)
    : IRequest<GetClientsFilterFullNameResponse>;