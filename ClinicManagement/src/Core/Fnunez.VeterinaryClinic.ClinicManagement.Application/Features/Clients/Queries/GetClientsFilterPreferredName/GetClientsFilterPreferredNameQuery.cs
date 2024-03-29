using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public record GetClientsFilterPreferredNameQuery(GetClientsFilterPreferredNameRequest GetClientsFilterPreferredNameRequest)
    : IRequest<GetClientsFilterPreferredNameResponse>;