using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;

public record GetClientsQuery(GetClientsRequest GetClientsRequest)
    : IRequest<GetClientsResponse>;